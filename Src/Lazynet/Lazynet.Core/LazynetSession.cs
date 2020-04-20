/*
* ==============================================================================
*
* Filename: LazynetSession
* Description: 
*
* Version: 1.0
* Created: 2020/4/18 17:18:36
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core
{
    /// <summary>
    /// session entity
    /// </summary>
    public class LazynetSession
    {
        public string ID { get; set; }
        public LazynetChannelHandlerContext Context { get; set; }
    }
}
