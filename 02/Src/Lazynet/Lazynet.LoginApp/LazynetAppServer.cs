/*
* ==============================================================================
*
* Filename: SocketClient
* Description: 
*
* Version: 1.0
* Created: 2020/5/23 0:11:35
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.AppCore;
using Lazynet.Core.Logger;
using Lazynet.Core.Network.Client;

namespace Lazynet.LoginApp
{
    /// <summary>
    ///  app network
    /// </summary>
    public class LazynetAppServer
    {
        public ILazynetClient Client { get; }
        public LazynetAppContext Context { get; }
        
        public LazynetAppServer(LazynetAppContext context)
        {
            this.Context = context;
            this.Client = new LazynetClient(this.Context.Config);
        }

        public void Connect()
        {
            this.Client.SetSocketEvent(new LazynetAppServerHandler(this.Context));
            bool connectResult = Client.WaitConnectToHost(3000, null);
            if (connectResult)
            {
                this.Context.Log(LazynetLogLevel.Info, "connect appmgr success");
            }
        }
    }
}
