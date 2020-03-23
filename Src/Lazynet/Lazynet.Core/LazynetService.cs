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
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lazynet.Core
{
    public class LazynetService
    {
        public ILazynetContext LazynetClient { get; set; }
        public LazynetServiceContext Context { get; set; }

        public LazynetService(int ID, ILazynetContext lazynetClient)
        {
            LazynetClient = lazynetClient;
            this.Context = new LazynetServiceContext {
                ID = ID,
                MessageQueue = new Queue<LazynetServiceMessage>(),
                ThreadHandle = new Thread(this.ProcessMessage),
                ManualEvent = new ManualResetEvent(true),
                State = LazynetServiceState.UnStart
            };
        }

        public LazynetService UseLua(string fileName, params KeyValuePair<string, object>[] args)
        {
            this.Context.Lua = new LazynetLua();
            this.Context.Lua.DoChunk(fileName, args);
            return this;
        }

        public LazynetService SendMessage(int serviceID, LazynetServiceMessage message)
        {
            LazynetClient.SendMessage(serviceID, message);
            return this;
        }

        public LazynetService SendMessage(LazynetServiceMessage message)
        {
            if (message is null)
            {
                throw new Exception("消息实体不能为null");
            }

            Context.MessageQueue.Enqueue(message);
            Context.SetState(LazynetServiceState.Runing);
            Context.ManualEvent.Set();
            return this;
        }

        public int GetID()
        {
            return this.Context.ID;
        }

        public LazynetServiceState GetState()
        {
            return Context.State;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="obj"></param>
        public void Start(object obj)
        {
            Context.SetState(LazynetServiceState.Start);
            Context.Start(obj);
        }

        /// <summary>
        /// 中断服务
        /// </summary>
        public void Interrupt()
        {
            this.Context.Interrupt();
        }

        private LazynetServiceMessage GetMessage()
        {
            var messageQueue = this.Context.MessageQueue;
            if (messageQueue.Count > 0)
            {
                return messageQueue.Dequeue();
            }
            else
            {
                return null;
            }
        }

        private void ProcessMessage(object obj)
        {
            Context.SetState(LazynetServiceState.Runing);
            while (true)
            {
                var messageEntity = this.GetMessage();
                if (messageEntity != null)
                {
                    var routeInfo = LazynetClient.Route.Mapping(messageEntity.RouteUrl);
                    if (routeInfo is null)
                    {
                        Console.WriteLine("route匹配失败");
                    }
                    else
                    {
                        routeInfo.CallMethod(this.Context, messageEntity);
                    }
                }
                else
                {
                    // 退出条件
                    if (Context.State == LazynetServiceState.Exit)
                    {
                        this.Exit();
                        break;
                    }

                    if (Context.ThreadHandle.ThreadState == ThreadState.Running)
                    {
                        Context.SetState(LazynetServiceState.Suspend);
                        Context.ManualEvent.Reset();
                        Context.ManualEvent.WaitOne();
                    }
                }
            }
        }

        private void Exit()
        {
            LazynetClient.RemoveService(this.GetID());
            Console.WriteLine($"【{this.GetID()}】:服务退出了");
        }

    }
}
