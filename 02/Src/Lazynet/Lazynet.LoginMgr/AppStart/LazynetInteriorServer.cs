/*
* ==============================================================================
*
* Filename: InteriorServer
* Description: 
*
* Version: 1.0
* Created: 2020/5/22 23:21:13
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Network;
using Lazynet.Core.Network.Server;
using Lazynet.Core.Proto;
using Lazynet.LoginMgr.AppStart;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginMgr
{
    /// <summary>
    /// 外部服务器
    /// </summary>
    public class LazynetInteriorServer
    {
        public LazynetAppContext Context { get; }
        public LazynetInteriorServerContext ServerContext { get; }

        public LazynetInteriorServer(LazynetAppContext context)
        {
            this.Context = context;
            this.ServerContext = new LazynetInteriorServerContext();
            this.ServerContext.Handler = new LazynetServer(new LazynetServerConfig()
            {
                Heartbeat = this.Context.Config.InteriorServerHeartbeat,
                Port = this.Context.Config.InteriorServerPort,
                SocketType = this.Context.Config.InteriorServerType,
                WebsocketPath = "/ws",
            });
            this.ServerContext.SessionDict = new Dictionary<string, LazynetSession>();
        }

        public void Start()
        {
            this.ServerContext.Handler.SetSocketEvent(new LazynetInteriorServerSocketEvent(this.Context, this.ServerContext));
            this.ServerContext.Handler.Bind();
            Context.Logger.Info("interior server bind port:" + ServerContext.Handler.GetPort());
        }

    }
}
