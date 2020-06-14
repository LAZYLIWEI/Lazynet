/*
* ==============================================================================
*
* Filename: Startup
* Description: 
*
* Version: 1.0
* Created: 2020/6/13 10:05:32
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.LoginMgr.AppStart;
using Lazynet.LoginMgr.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginMgr
{
    public class Startup : ILazynetStartup
    {
        public void Configuration(LazynetAppConfig config)
        {
            config.ExternalServerHeartbeat = 3000;
            config.ExternalServerPort = 20000;
            config.ExternalServerType = Core.Network.LazynetSocketType.TcpSocket;

            config.InteriorServerHeartbeat = 3000;
            config.InteriorServerPort = 10000;
            config.InteriorServerType = Core.Network.LazynetSocketType.Websocket;
        }

        public void ConfigureFilter(LazynetAppFilter filters)
        {
            filters.ExpcetionFilter = new MyExptionFilter();
            filters.ActionFilter = new MyActionFilter();
        }
    }
}
