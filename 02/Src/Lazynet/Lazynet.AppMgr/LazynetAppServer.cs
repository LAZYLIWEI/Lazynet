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

namespace Lazynet.AppMgr
{
    /// <summary>
    /// 服务器
    /// </summary>
    public class LazynetAppServer
    {
        public LazynetAppContext Context { get; }
        public ILazynetServer Server { get; set; }

        public LazynetAppServer(LazynetAppContext context)
        {
            // 上下文
            this.Context = context;
            this.Server =  new LazynetServer(new LazynetServerConfig() {
                Heartbeat = this.Context.Config.Heartbeat,
                Port = this.Context.Config.Port,
                SocketType = this.Context.Config.SocketType
            });
        }

        public void Start()
        {
            this.Server.SetSocketEvent(new LazynetServerHandler(this.Context));
            this.Server.Bind();
        }

    }
}
