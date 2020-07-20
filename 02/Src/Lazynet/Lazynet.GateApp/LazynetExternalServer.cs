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
using Lazynet.Core.Network.Server;

namespace Lazynet.GateApp
{
    /// <summary>
    /// 外部服务器
    /// </summary>
    public class LazynetExternalServer
    {
        public LazynetAppContext Context { get; }
        public ILazynetServer Server { get; set; }

        public LazynetExternalServer(LazynetAppContext context)
        {
            // 上下文
            this.Context = context;
            this.Server = new LazynetServer(new LazynetServerConfig()
            {
                Heartbeat = this.Context.Config.ExternalServerHeartbeat,
                Port = this.Context.Config.ExternalServerPort,
                SocketType = this.Context.Config.ExternalServerType,
                WebsocketPath = this.Context.Config.ExternalServerWebsocketPath
            });
        }

        public void Start()
        {
            this.Server.SetSocketEvent(new LazynetExternalServerHandler(this.Context));
            this.Server.Bind();
        }
    }
}
