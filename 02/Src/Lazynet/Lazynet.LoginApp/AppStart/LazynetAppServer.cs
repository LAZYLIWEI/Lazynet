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
using Lazynet.Core.Network;
using Lazynet.Core.Network.Client;
using Lazynet.Core.Service;
using Lazynet.Core.Timer;
using Lazynet.Core.Util;
using Quartz;
using System;
using System.Collections.Generic;

namespace Lazynet.LoginApp.AppStart
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
            this.Client = new LazynetClient(this.Context.Config.NetworkConfig);
            
        }

        public void Connect()
        {
            Client.SetSocketEvent(new LazynetAppServerHandler(this.Context));
            bool connectResult = Client.WaitConnectToHost(3000, str=> {
                this.Context.Logger.Log("连接失败");
            });
            if (connectResult)
            {
                this.Context.Logger.Log("连接成功");
            }
        }

    }
}
