/*
* ==============================================================================
*
* Filename: LazynetServiceMeta
* Description: 
*
* Version: 1.0
* Created: 2020/5/29 22:31:49
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lazynet.Core.Service
{
    public class LazynetServiceMeta
    {
        public Type ClassType { get; set; }
        public MethodInfo MethodType { get; set; }
        public string RouteUrl { get; set; }
    }
}
