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
using Lazynet.AppCore;
using Lazynet.Core.Network.Client;

namespace Lazynet.GateApp
{
    /// <summary>
    /// 外部服务器
    /// </summary>
    public class LazynetInteriorServer
    {
        public LazynetAppContext Context { get; }
        public ILazynetClient Client { get; }

        public LazynetInteriorServer(LazynetAppContext context)
        {
            this.Context = context;
            this.Client = new LazynetClient(new LazynetClientConfig()
            {
                IP = this.Context.Config.InteriorServerIP,
                Port = this.Context.Config.InteriorServerPort,
                SocketType = this.Context.Config.InteriorServerType
            });
        }

        public void Start()
        {
            this.Client.SetSocketEvent(new LazynetInteriorServerHandler(this.Context));
            bool connectResult = Client.WaitConnectToHost(3000, null);
            if (connectResult)
            {
                //this.Context.Log(LazynetLogLevel.Info, "connect appmgr success");
            }
        }

    }
}
