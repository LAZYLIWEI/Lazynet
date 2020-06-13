/*
* ==============================================================================
*
* Filename:LazynetHandler
* Description: 
*
* Version: 1.0
* Created: 2020/3/29 17:19:10
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

namespace Lazynet.Core
{
    /// <summary>
    /// trigger
    /// </summary>
    public abstract class LazynetTriggerProvider
    {
        public ILazynetService ServiceContext { get; }

        protected LazynetTriggerProvider(ILazynetService context)
        {
            this.ServiceContext = context;
        }
    }
}
