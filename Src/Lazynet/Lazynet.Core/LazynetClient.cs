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
        #region private
        /// <summary>
        /// 服务ID
        /// </summary>
        private int globaServiceID;

        /// <summary>
        /// 全局消息入队锁
        /// </summary>
        private static object globaMessageQueueLock;
        #endregion

        #region public
        /// <summary>
        /// 配置
        /// </summary>
        public LazynetConfig Config { get; set; }

        /// <summary>
        /// 服务
        /// </summary>
        public Dictionary<int, LazynetService> ServiceDictionary { get; }

        /// <summary>
        /// 消息队列
        /// </summary>
        public Queue<LazynetGlobaMessage> GlobaMessageQueue { get; }

        /// <summary>
        /// 消息线程
        /// </summary>
        public Thread MessageThread { get; }

        /// <summary>
        /// 消息事件
        /// </summary>
        public ManualResetEvent MessageEvent { get; set; }

        /// <summary>
        /// 日志
        /// </summary>
        public ILazynetLogger Logger { get; set; }

        #endregion

        #region constructed
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public LazynetClient(LazynetConfig config)
        {
            this.Config = config;
            this.GlobaMessageQueue = new Queue<LazynetGlobaMessage>();
            this.ServiceDictionary = new Dictionary<int, LazynetService>();
            this.globaServiceID = 100000;
            this.MessageThread = new Thread(this.DeliveryMessage);
            this.MessageEvent = new ManualResetEvent(false);
            this.Logger = new LazynetLogger();
            globaMessageQueueLock = new object();
        }

        #endregion

        #region service
        public ILazynetService GetService(int serviceID)
        {
            if (this.ServiceDictionary.ContainsKey(serviceID))
            {
                return this.ServiceDictionary[serviceID];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取全局服务ID
        /// </summary>
        /// <returns></returns>
        public int GetGlobaServiceID()
        {
            this.globaServiceID++;
            return this.globaServiceID;
        }

        /// <summary>
        /// 获取服务Id
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public int GetServiceID(string alias)
        {
            foreach (var item in ServiceDictionary.Values)
            {
                if (item.Alias == alias)
                {
                    return item.ID;
                }
            }

            return 0;
        }

        /// <summary>
        /// 创建服务
        /// </summary>
        /// <returns></returns>
        public LazynetService CreateSharpService()
        {
            LazynetService service = new LazynetSharpService(this);
            this.ServiceDictionary.Add(service.ID, service);
            return service;
        }

        /// <summary>
        /// 删除服务
        /// </summary>
        /// <param name="serviceID"></param>
        public void RemoveService(int serviceID)
        {
            this.ServiceDictionary.Remove(serviceID);
        }

        /// <summary>
        /// 创建lua服务
        /// </summary>
        /// <param name="filename">lua文件路径</param>
        /// <returns></returns>
        public LazynetLuaService CreateLuaService(string filename)
        {
            LazynetLuaService luaService = new LazynetLuaService(this, filename);
            this.ServiceDictionary.Add(luaService.ID, luaService);
            return luaService;
        }
        #endregion

        #region message

        /// <summary>
        /// 开始分发消息
        /// </summary>
        /// <returns></returns>
        public LazynetClient DispatchMessage()
        {
            this.MessageThread.Start();
            return this;
        }

        /// <summary>
        /// 接收从service发过来的消息
        /// </summary>
        /// <param name="serviceID">服务id</param>
        /// <param name="serviceMessage">服务消息</param>
        public void RecvMessage(int serviceID, LazynetServiceMessage serviceMessage)
        {
            if (!this.ServiceDictionary.ContainsKey(serviceID))
            {
                this.Logger.Info(serviceID.ToString(), "不包含这个id");
                return;
            }
            if (serviceMessage is null)
            {
                this.Logger.Info(serviceID.ToString(), "消息实体为空");
                return;
            }
            var globaMessage = new LazynetGlobaMessage() {
                ServiceID = serviceID,
                ServiceMessage = serviceMessage
            };
            lock(globaMessageQueueLock)
            {
                this.GlobaMessageQueue.Enqueue(globaMessage);
            }
            this.MessageEvent.Set();
        }

        /// <summary>
        /// 从队列获取消息
        /// </summary>
        /// <returns></returns>
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
        /// 投递消息
        /// </summary>
        private void DeliveryMessage()
        {
            while (true)
            {
                var message = this.GetMessage();
                if (message != null)
                {
                    var service = this.ServiceDictionary[message.ServiceID];
                    if (service != null)
                    {
                        service.RecvMessage(message.ServiceMessage);
                    }
                }
                else
                {
                    this.MessageEvent.Reset();
                    this.MessageEvent.WaitOne();
                }
            }
        }




        #endregion
       
    }
}
