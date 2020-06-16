/*
* ==============================================================================
*
* Filename: LazynetAppFilter
* Description: 
*
* Version: 1.0
* Created: 2020/6/13 19:34:53
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.AppMgrCore
{
    public class LazynetAppFilter
    {
        public ILazynetServiceExceptionFilter ExpcetionFilter { get; set; }
        public ILazynetServiceActionFilter ActionFilter { get; set; }
    }
}
