/*
* ==============================================================================
*
* Filename: ExternalServer
* Description: 
*
* Version: 1.0
* Created: 2020/5/22 0:02:15
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Logger;
using Lazynet.Core.Network.Server;
using Lazynet.Core.Proto;
using Lazynet.Core.Service;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginMgr
{
    /// <summary>
    /// 内部服务器
    /// </summary>
    public class ExternalServer
    {
        private int globaIndex;
        public ILazynetServer Server { get; }
        public Dictionary<string, ILazynetService> ServiceDict { get; }
        public ChildNodeCollection ChildNodeCollection { get; }
        public event Action<string, string> DispatchEventNotify;

        public ExternalServer()
        {
            this.Server =  new LazynetServer(new LazynetServerConfig()
            {
                Heartbeat = 3000,
                Port = 20000,
                SocketType = Core.Network.LazynetSocketType.TcpSocket,
            });
            this.ServiceDict = new Dictionary<string, ILazynetService>();
            this.ChildNodeCollection = new ChildNodeCollection();
            this.globaIndex = 0;
        }

        public void Start()
        {
            this.AddService(new ExternalServerHandler(this.ChildNodeCollection, this.DispatchEventNotify));
            Server.SetSocketEvent(new ExternalServerSocketEvent(this.ServiceDict));
            Server.Bind();
            LoggerMgr.GetInstance().Log("external server bind port:" + Server.GetPort());
        }

        public void AddService(ExternalServerHandler handler)
        {
            var methods = handler.GetType().GetMethods();
            foreach (var item in methods)
            {
                ILazynetService externalTrigger = new LazynetSharpService(LazynetServiceType.Normal, handler, item);
                this.ServiceDict.Add(item.Name, externalTrigger);
            }
        }


        public void NoticeDispatchEventNotify(Action<string, string> action)
        {
            this.DispatchEventNotify += action;
        }


        public void Dispatch(LazynetMessage message)
        {
            if (this.ChildNodeCollection.Count() <= 0)
            {
                LoggerMgr.GetInstance().Log("child node's count is zero");
                return;
            }

            ChildNode childNode = null;
            int i = 0;
            foreach (var item in this.ChildNodeCollection.ChildNodeEntityDict)
            {
                int index = globaIndex % this.ChildNodeCollection.Count();
                if (i == index)
                {
                    childNode = item.Value;
                    break;
                }
                i++;
            }
            if (globaIndex >= int.MaxValue)
            {
                globaIndex = 0;
            }
            else
            {
                globaIndex++;
            }

            if (childNode == null)
            {
                LoggerMgr.GetInstance().Log("don't have the child node");
                return;
            }
            
            childNode.Context.WriteAndFlushAsync(SerializeHelper.Serialize(message));
        }

       

       

    }
}
