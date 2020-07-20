/*
* ==============================================================================
*
* Filename: LazynetSession
* Description: 
*
* Version: 1.0
* Created: 2020/5/24 11:29:53
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
    public class LazynetSession
    {
        public string ID { get; set; }
        public string Address { get; set; }
        public LazynetHandlerContext Context { get; set; }
        public DateTime ConnectDateTime { get; set; }
    }
}
