/*
* ==============================================================================
*
* Filename: LazynetServiceRequest
* Description: 
*
* Version: 1.0
* Created: 2020/5/31 21:43:32
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

namespace Lazynet.Core.Service
{
    public class LazynetServiceRequest
    {
        public string RouteUrl { get; set; }
        public string SessionID { get; set; }
        public LazynetHandlerContext Handler { get; set; }
    }
}
