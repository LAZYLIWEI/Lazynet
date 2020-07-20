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

namespace Lazynet.AppMgr
{
    public class LazynetAppConfig
    {
        public int Port { get; set; }
        public int Heartbeat { get; set; }
        public LazynetSocketType SocketType { get; set; }
    }
}
