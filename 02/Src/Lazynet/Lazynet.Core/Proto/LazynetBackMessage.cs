/*
* ==============================================================================
*
* Filename: LazynetBackMessage
* Description: 
*
* Version: 1.0
* Created: 2020/6/1 1:31:12
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
    public class LazynetBackMessage
    {
        public string RouteUrl { get; set; }
        public object Parameter { get; set; }
    }
}
