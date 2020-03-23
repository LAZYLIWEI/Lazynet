/*
* ==============================================================================
*
* Filename: LazynetServer
* Description: 
*
* Version: 1.0
* Created: 2020/3/22 13:49:57
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Lazynet.Core
{
    /// <summary>
    /// lazynet服务
    /// </summary>
    public class LazynetClient : ILazynetContext
    {
        /// <summary>
        /// 配置
        /// </summary>
        public LazynetConfig Config { get; set; }

        /// <summary>
        /// 服务ID
        /// </summary>
        public int ServiceID { get; set; }

        /// <summary>
        /// 服务
        /// </summary>
        public Dictionary<int, LazynetService> ServiceDictionary { get; set; }

        /// <summary>
        /// 消息队列
        /// </summary>
        public Queue<LazynetGlobaMessage> GlobaMessageQueue { get; set; }

        /// <summary>
        /// 消息线程
        /// </summary>
        public Thread MessageThread { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
        public LazynetRoute Route { get; set; }

        /// <summary>
        /// 消息事件
        /// </summary>
        public ManualResetEvent MessageEvent { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public LazynetClient(LazynetConfig config)
        {
            this.Config = config;
            this.GlobaMessageQueue = new Queue<LazynetGlobaMessage>();
            this.ServiceDictionary = new Dictionary<int, LazynetService>();
            this.ServiceID = 100000;
            this.MessageThread = new Thread(this.ProcessMessage);
            this.Route = new LazynetRoute();
            this.MessageEvent = new ManualResetEvent(false);
        }

        public void DispatchMessage()
        {
            this.MessageThread.Start();
        }

        public LazynetClient UseRoute(Assembly ass)
        {
            this.Route.GetHandlerList(ass);
            return this;
        }

        private LazynetGlobaMessage GetMessage()
        {
            if (this.GlobaMessageQueue.Count > 0)
            {
                return this.GlobaMessageQueue.Dequeue();
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// 处理消息
        /// </summary>
        private void ProcessMessage()
        {
            while (true)
            {
                var message = this.GetMessage();
                if (message != null)
                {
                    var service = this.ServiceDictionary[message.ServiceID];
                    if (service != null)
                    {
                        service.SendMessage(message.ServiceMessage);
                    }
                }
                else
                {
                    this.MessageEvent.Reset();
                    this.MessageEvent.WaitOne();
                }
            }
        }
       
        public LazynetService CreateService()
        {
            LazynetService service = new LazynetService(this.ServiceID, this);
            this.ServiceDictionary.Add(this.ServiceID, service);
            this.ServiceID++;
            return service;
        }

        public void RemoveService(int serviceID)
        {
            this.ServiceDictionary.Remove(serviceID);
        }

        public void SendMessage(int serviceID, LazynetServiceMessage serviceMessage)
        {
            this.GlobaMessageQueue.Enqueue(new LazynetGlobaMessage() { 
                 ServiceID = serviceID,
                 ServiceMessage = serviceMessage
            });
            this.MessageEvent.Set();
        }
    }
}
