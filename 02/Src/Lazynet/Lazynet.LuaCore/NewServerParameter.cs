/*
* ==============================================================================
*
* Filename: NewServerParameter
* Description: 
*
* Version: 1.0
* Created: 2020/5/4 14:06:10
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

namespace Lazynet.LuaCore
{
    public class NewServerParameter
    {
        public int port { get; set; }
        public int heartbeat { get; set; }
        public LazynetSocketType socketType { get; set; }
        public string websocketPath { get; set; }
    }
}
