﻿/*
* ==============================================================================
*
* Filename: ChildNodeEntity
* Description: 
*
* Version: 1.0
* Created: 2020/5/22 23:34:47
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

namespace Lazynet.LoginMgr
{
    /// <summary>
    /// child node server
    /// </summary>
    public class ChildNode
    {
        public string Address { get; set; }
        public LazynetHandlerContext Context { get; set; }
        public DateTime ConnectDateTime { get; set; }
    }
}
