/*
* ==============================================================================
*
* Filename: LazynetSessionMessage
* Description: 
*
* Version: 1.0
* Created: 2020/7/12 19:23:34
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.GateApp
{
    public class LazynetSessionMessage
    {
        public string ServerName { get; set; }
        public string RouteUrl { get; set; }
        public object[] Parameters { get; set; }
    }
}
