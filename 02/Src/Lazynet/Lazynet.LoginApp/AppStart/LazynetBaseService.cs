/*
* ==============================================================================
*
* Filename: LazynetBaseService
* Description: 
*
* Version: 1.0
* Created: 2020/5/29 21:54:50
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

namespace Lazynet.LoginApp.AppStart
{
    /// <summary>
    /// base service
    /// </summary>
    public abstract class LazynetBaseService : ILazynetApiService
    {
        public LazynetAppContext Context { get;}
        public LazynetBaseService(LazynetAppContext context)
        {
            this.Context = context;
        }
    }
}
