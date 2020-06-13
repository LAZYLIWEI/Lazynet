/*
* ==============================================================================
*
* Filename: NewClientParameter
* Description: 
*
* Version: 1.0
* Created: 2020/5/5 12:01:45
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
    public class NewClientParameter
    {
        public string ip { get; set; }
        public int port { get; set; }
        public LazynetSocketType socketType { get; set; }
        public string websocketPath { get; set; }
    }
}
