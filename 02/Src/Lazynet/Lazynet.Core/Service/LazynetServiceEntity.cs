/*
* ==============================================================================
*
* Filename: LazynetTriggerEntity
* Description: 
*
* Version: 1.0
* Created: 2020/5/5 9:42:16
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Service
{
    public class LazynetServiceEntity
    {
        public LazynetServiceType Type { get; set; }
        public string RouteUrl { get; set; }
        public object[] Parameters { get; set; }
    }
}
