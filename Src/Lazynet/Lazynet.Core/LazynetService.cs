/*
* ==============================================================================
*
* Filename: LazynetService
* Description: 
*
* Version: 1.0
* Created: 2020/3/22 14:27:23
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.LUA;
using Lazynet.Network;
using Lazynet.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lazynet.Core
{
    public abstract class LazynetService : ILazynetService
    {
        #region filed
        private readonly object stateLock = new object();
        #endregion

        #region Property
        public int ID { get; set; }
        public string Alias { get; set; }
        public Thread ThreadHandle { get; set; }
        public Queue<LazynetServiceMessage> MessageQueue { get; set; }
        public ManualResetEvent ManualEvent { get; set; }
        public LazynetServiceState State { get; set; }
        public Dictionary<string, ILazynetTrigger> NormalTriggerDict { get; set; }
        public Dictionary<string, ILazynetTrigger> SystemTriggerDict { get; set; }
        public ILazynetContext Context { get; set; }
        public ILazynetSocketContext Socket { get; set; }
        public LazynetSocketEvent SocketEvent { get; set; }
        public ILazynetSessionGroup SessionGroup { get; set; }
        #endregion

        #region constructed
        public LazynetService(ILazynetContext context)
        {
            this.Context = context;
            this.ID = context.GetGlobaServiceID();
            this.MessageQueue = new Queue<LazynetServiceMessage>();
            this.ThreadHandle = new Thread(this.ThreadMain);
            this.ManualEvent = new ManualResetEvent(true);
            this.State = LazynetServiceState.UnStart;
            this.NormalTriggerDict = new Dictionary<string, ILazynetTrigger>();
            this.SystemTriggerDict = new Dictionary<string, ILazynetTrigger>();
            this.SessionGroup = new LazynetDefaultSessionGroup();
            this.RegisterSystemTrigger();
        }
        #endregion

        #region abstract
        public abstract void Start();
        #endregion

        #region net 
        public void CreateSocket(LazynetSocketConfig config)
        {
            this.Socket = new LazynetSocket(config);
        }

        public void BindAsync(LazynetSocketEvent socketEvent)
        {
            if (this.Socket is null)
            {
                throw new Exception("请先create socket, 再调用此方法");
            }
            this.SocketEvent = socketEvent;
            this.Socket.SetEvent(new LazynetDefaultSocketEvent(this));
            this.Socket.BindAsync();
        }

        public void CloseSocket()
        {
            this.Socket?.Close();
        }

        #endregion

        #region session
        public void SetSessionGroup(ILazynetSessionGroup sessionGroup)
        {
            this.SessionGroup = sessionGroup;
        }

        public void AddSession(LazynetChannelHandlerContext channelHandlerContext)
        {
            var session = new LazynetSession()
            {
                Context = channelHandlerContext,
                ID = SnowflakeUtil.Instance().GetString()
            };
            this.SessionGroup.Add(session);
        }

        public void AddSession(LazynetSession session)
        {
            if (this.SessionGroup is null)
            {
                throw new Exception("session group未初始化");
            }
            this.SessionGroup.Add(session);
        }

        public void RemoveSession(LazynetChannelHandlerContext channelHandlerContext)
        {
            var session = this.SessionGroup.Find(channelHandlerContext);
            this.SessionGroup.Remove(session);
        }
       
        public void RemoveSession(LazynetSession session)
        {
            if (this.SessionGroup is null)
            {
                throw new Exception("session group未初始化");
            }
            this.SessionGroup.Remove(session);
        }

        public void RemoveSession(string ID)
        {
            if (this.SessionGroup is null)
            {
                throw new Exception("session group未初始化");
            }
            var session = this.SessionGroup.Find(ID);
            if (session != null)
            {
                this.SessionGroup.Remove(session);
            }
        }

        public void ClearSession()
        {
            if (this.SessionGroup is null)
            {
                throw new Exception("session group未初始化");
            }
            this.SessionGroup.Clear();
        }

        public LazynetSession FindSession(string ID)
        {
            if (this.SessionGroup is null)
            {
                throw new Exception("session group未初始化");
            }
            return this.SessionGroup.Find(ID);
        }
        #endregion

        #region trigger
        /// <summary>
        /// 添加触发器
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="trigger">触发器</param>
        public void AddTrigger(string command, ILazynetTrigger trigger)
        {
            if (trigger is null)
            {
                throw new Exception("trigger value is null");
            }
            if (this.NormalTriggerDict.ContainsKey(command))
            {
                this.NormalTriggerDict[command] = trigger;
            }
            else
            {
                this.NormalTriggerDict.Add(command, trigger);
            }
        }

        /// <summary>
        /// 移除触发器
        /// </summary>
        /// <param name="command">命令</param>
        public bool RemoveTrigger(string command)
        {
            if (!this.NormalTriggerDict.ContainsKey(command))
            {
                return false;
            }
            return this.NormalTriggerDict.Remove(command);
        }

        /// <summary>
        /// 清空触发器
        /// </summary>
        public void ClearTrigger()
        {
            this.NormalTriggerDict.Clear();
        }


        /// <summary>
        /// 注册系统的trigger
        /// </summary>
        private void RegisterSystemTrigger()
        {
            LazynetSystemTrigger systemTrigger = new LazynetSystemTrigger(this);
            this.SystemTriggerDict.Add("startService",  new LazynetSharpTrigger<LazynetSystemTrigger>(systemTrigger, systemTrigger.GetType().GetMethod("StartService")));
            this.SystemTriggerDict.Add("writeAndFlush",  new LazynetSharpTrigger<LazynetSystemTrigger>(systemTrigger, systemTrigger.GetType().GetMethod("WriteAndFlush")));
            this.SystemTriggerDict.Add("write",  new LazynetSharpTrigger<LazynetSystemTrigger>(systemTrigger, systemTrigger.GetType().GetMethod("Write")));
            this.SystemTriggerDict.Add("setSessionGroup",  new LazynetSharpTrigger<LazynetSystemTrigger>(systemTrigger, systemTrigger.GetType().GetMethod("SetSessionGroup")));
            this.SystemTriggerDict.Add("addSession",  new LazynetSharpTrigger<LazynetSystemTrigger>(systemTrigger, systemTrigger.GetType().GetMethod("AddSession")));
            this.SystemTriggerDict.Add("removeSession",  new LazynetSharpTrigger<LazynetSystemTrigger>(systemTrigger, systemTrigger.GetType().GetMethod("RemoveSession")));
            this.SystemTriggerDict.Add("clearSession",  new LazynetSharpTrigger<LazynetSystemTrigger>(systemTrigger, systemTrigger.GetType().GetMethod("ClearSession")));
            this.SystemTriggerDict.Add("log",  new LazynetSharpTrigger<LazynetSystemTrigger>(systemTrigger, systemTrigger.GetType().GetMethod("Log")));
            this.SystemTriggerDict.Add("createSocket",  new LazynetSharpTrigger<LazynetSystemTrigger>(systemTrigger, systemTrigger.GetType().GetMethod("CreateSocket")));
            this.SystemTriggerDict.Add("bind",  new LazynetSharpTrigger<LazynetSystemTrigger>(systemTrigger, systemTrigger.GetType().GetMethod("BindAsync")));
            this.SystemTriggerDict.Add("closeSocket",  new LazynetSharpTrigger<LazynetSystemTrigger>(systemTrigger, systemTrigger.GetType().GetMethod("CloseSocket")));
        }

        #endregion

        #region info
        /// <summary>
        /// 设置别名
        /// </summary>
        /// <param name="alias">别名</param>
        public void SetAlias(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                throw new Exception("alias is null or empty");
            }
            this.Alias = alias;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="action"></param>
        protected void Start(Action action)
        {
            this.SetState(LazynetServiceState.Start);
            this.ThreadHandle.Start(action);
        }

        /// <summary>
        /// 获取服务状态
        /// </summary>
        /// <returns></returns>
        protected LazynetServiceState GetState()
        {
            return this.State;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="state"></param>
        protected void SetState(LazynetServiceState state)
        {
            lock (stateLock)
            {
                this.State = state;
            }
        }

        /// <summary>
        /// 中断服务
        /// </summary>
        public void Interrupt()
        {
            this.SendMessage(this.ID, new LazynetServiceMessage(LazynetMessageType.System, "exit", null));
        }

        /// <summary>
        /// 中断服务
        /// </summary>
        /// <param name="ID"></param>
        public void Kill(int ID)
        {
            this.SendMessage(ID, new LazynetServiceMessage(LazynetMessageType.System, "exit", null));
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="serviceId">服务id</param>
        public void StartService(int serviceId)
        {
            this.SendMessage(this.ID, new LazynetServiceMessage(LazynetMessageType.System, "start", new object[] { serviceId }));
        }
        #endregion

        #region message
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="serviceID">服务号</param>
        /// <param name="message">消息实体</param>
        /// <returns></returns>
        protected LazynetService SendMessage(int serviceID, LazynetServiceMessage message)
        {
            this.Context.RecvMessage(serviceID, message);
            return this;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public LazynetService RecvMessage(LazynetServiceMessage message)
        {
            this.MessageQueue.Enqueue(message);
            this.SetState(LazynetServiceState.Runing);
            this.ManualEvent.Set();
            return this;
        }

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <returns></returns>
        private LazynetServiceMessage GetMessage()
        {
            var messageQueue = this.MessageQueue;
            if (messageQueue.Count > 0)
            {
                return messageQueue.Dequeue();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 线程主函数
        /// </summary>
        /// <param name="parameter"></param>
        private void ThreadMain(object parameter)
        {
            // 回调
            this.SetState(LazynetServiceState.Runing);
            if (parameter is Action)
            {
                var action = parameter as Action;
                Task.Run(action);
            }
            
            // 处理消息
            this.ProcessMessage();
        }

        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="obj"></param>
        private void ProcessMessage()
        {
            while (true)
            {
                LazynetServiceMessage messageEntity = this.GetMessage();
                if (messageEntity != null)
                {
                    if (messageEntity.Type == LazynetMessageType.System)
                    {
                        int result = ProcessSystemMessage(messageEntity);
                        if (result == 1)
                        {
                            break;
                        }
                    }
                    else if (messageEntity.Type == LazynetMessageType.Normal)
                    {
                        ProcessNormalMessage(messageEntity);
                    }
                    else
                    {
                        // other消息
                        Context.Logger.Info(this.ID.ToString(), "no type message");
                    }
                }
                else
                {
                    if (this.State == LazynetServiceState.Runing)
                    {
                        this.SetState(LazynetServiceState.Suspend);
                        this.ManualEvent.Reset();
                        this.ManualEvent.WaitOne();
                    }
                }
            }
        }

        private int ProcessSystemMessage(LazynetServiceMessage msg)
        {
            // 系统消息
            if (msg.RouteUrl.Equals("exit"))
            {
                this.SetState(LazynetServiceState.Exit);
                this.ExitCallback();
                return 1;
            }

            if (this.SystemTriggerDict.ContainsKey(msg.RouteUrl))
            {
                var trigger = this.SystemTriggerDict[msg.RouteUrl];
                trigger.CallBack(msg);
            }
            else
            {
                Context.Logger.Info(this.ID.ToString(), "no mapping");
            }

            return 0;
        }

        private void ProcessNormalMessage(LazynetServiceMessage msg)
        {
            if (this.NormalTriggerDict.ContainsKey(msg.RouteUrl))
            {
                var trigger = this.NormalTriggerDict[msg.RouteUrl];
                trigger.CallBack(msg);
            }
            else
            {
                Context.Logger.Info(this.ID.ToString(), "no mapping");
            }
        }

        /// <summary>
        /// 退出回调
        /// </summary>
        private void ExitCallback()
        {
            this.CloseSocket();
            Context.RemoveService(this.ID);
            Context.Logger.Info(this.ID.ToString(), "kill self");
        }

        #endregion
    }
}
