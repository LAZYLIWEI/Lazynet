/*
* ==============================================================================
*
* Filename: LazynetAppConfig
* Description: 
*
* Version: 1.0
* Created: 2020/6/13 10:11:56
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.GateApp
{
    public class LazynetAppConfig
    {
        public int ExternalServerPort { get; set; }
        public int ExternalServerHeartbeat { get; set; }
        public LazynetSocketType ExternalServerType { get; set; }
        public string ExternalServerWebsocketPath { get; set; }

        public string InteriorServerIP { get; set; }
        public int InteriorServerPort { get; set; }
        public LazynetSocketType InteriorServerType { get; set; }
        
    }
}
