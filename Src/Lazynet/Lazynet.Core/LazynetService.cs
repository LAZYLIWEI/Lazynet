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
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lazynet.Core
{
    public abstract class LazynetService : ILazynetService
    {
        private static object stateLock = new object();
        public int ID { get; set; }
        public string Alias { get; set; }
        public Thread ThreadHandle { get; set; }
        public Queue<LazynetServiceMessage> MessageQueue { get; set; }
        public ManualResetEvent ManualEvent { get; set; }
        public LazynetServiceState State { get; set; }
        public Dictionary<string, ILazynetTrigger> TriggerDict { get; set; }
        public ILazynetContext Context { get; set; }


        #region constructed
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public LazynetService(ILazynetContext context)
        {
            this.Context = context;
            this.ID = context.GetGlobaServiceID();
            this.MessageQueue = new Queue<LazynetServiceMessage>();
            this.ThreadHandle = new Thread(this.ProcessMessage);
            this.ManualEvent = new ManualResetEvent(true);
            this.State = LazynetServiceState.UnStart;
            this.TriggerDict = new Dictionary<string, ILazynetTrigger>();
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

            this.TriggerDict.Add(command, trigger);
        }

        /// <summary>
        /// 移除触发器
        /// </summary>
        /// <param name="command">命令</param>
        public bool RemoveTrigger(string command)
        {
            if (!this.TriggerDict.ContainsKey(command))
            {
                return false;
            }
            return this.TriggerDict.Remove(command);
        }

        /// <summary>
        /// 清空触发器
        /// </summary>
        public void ClearTrigger()
        {
            this.TriggerDict.Clear();
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
        public void Start()
        {
            this.Start(null);
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="obj"></param>
        public void Start(object obj)
        {
            this.SetState(LazynetServiceState.Start);
            this.ThreadHandle.Start(obj);
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
            lock(stateLock)
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
        /// 处理消息
        /// </summary>
        /// <param name="obj"></param>
        private void ProcessMessage(object obj)
        {
            this.SetState(LazynetServiceState.Runing);
            while (true)
            {
                LazynetServiceMessage messageEntity = this.GetMessage();
                if (messageEntity != null)
                {
                    if (messageEntity.Type == LazynetMessageType.System)
                    {
                        // 系统消息
                        if (messageEntity.RouteUrl.Equals("exit"))
                        {
                            this.SetState(LazynetServiceState.Exit);
                            this.ExitCallback();
                            break;
                        }
                    }
                    else if (messageEntity.Type == LazynetMessageType.Lua)
                    {
                        // lua消息
                        if (this.TriggerDict.ContainsKey(messageEntity.RouteUrl))
                        {
                            var trigger = this.TriggerDict[messageEntity.RouteUrl];
                            trigger.CallBack(messageEntity);
                        }
                        else
                        {
                            Context.Logger.Info(this.ID.ToString(), "no mapping");
                        }
                    }
                    else if (messageEntity.Type == LazynetMessageType.Socket)
                    {
                        // socket消息
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

        /// <summary>
        /// 退出回调
        /// </summary>
        private void ExitCallback()
        {
            Context.RemoveService(this.ID);
            Context.Logger.Info(this.ID.ToString(), "服务退出了");
        }

        #endregion

    }
}
