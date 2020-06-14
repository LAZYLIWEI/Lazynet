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

namespace Lazynet.LoginMgr.AppStart
{
    /// <summary>
    /// 内部服务器
    /// </summary>
    public class LazynetExternalServer
    {
        
        public LazynetAppContext Context { get; }
        public LazynetExternalServerContext ServerContext { get; }
      
        public LazynetExternalServer(LazynetAppContext context)
        {
            // 上下文
            this.Context = context;
            this.ServerContext = new LazynetExternalServerContext();
            this.ServerContext.Handler =  new LazynetServer(new LazynetServerConfig() {
                Heartbeat = this.Context.Config.ExternalServerHeartbeat,
                Port = this.Context.Config.ExternalServerPort,
                SocketType = this.Context.Config.ExternalServerType
            });
            this.ServerContext.ServiceDict = new Dictionary<string, ILazynetService>();
            this.ServerContext.ChildNodeCollection = new LazynetChildNodeCollection();
            this.ServerContext.GlobaIndex = 0;
        }

        public void Start()
        {
            // 添加service
            this.AddService(new LazynetExternalServerHandler(this.ServerContext, this.Context));

            this.ServerContext.Handler.SetSocketEvent(new LazynetExternalServerSocketEvent(this.ServerContext, this.Context));
            this.ServerContext.Handler.Bind();
            this.Context.Logger.Info("external server bind port:" + this.ServerContext.Handler.GetPort());
        }

        public void AddService(LazynetExternalServerHandler handler)
        {
            var methods = handler.GetType().GetMethods();
            foreach (var item in methods)
            {
                ILazynetService externalTrigger = new LazynetSharpService(LazynetServiceType.Normal, handler, item);
                this.ServerContext.ServiceDict.Add(item.Name, externalTrigger);
            }
        }
      

    }
}
