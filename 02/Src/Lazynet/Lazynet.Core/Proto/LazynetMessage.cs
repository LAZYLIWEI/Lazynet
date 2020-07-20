/*
* ==============================================================================
*
* Filename: LazynetMessage
* Description: 
*
* Version: 1.0
* Created: 2020/5/30 23:07:38
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

namespace Lazynet.Core.Proto
{
    public class LazynetMessage
    {
        public string RouteUrl { get; set; }
        public List<object> Parameters { get; set; }
        public LazynetMessage()
        {
            this.Parameters = new List<object>();
        }
    }
}
