/*
* ==============================================================================
*
* Filename: MyActionFilter
* Description: 
*
* Version: 1.0
* Created: 2020/5/31 1:12:11
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.AppMgrCore;
using Lazynet.Core.Proto;
using Lazynet.Core.Service;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginMgr.Filter
{
    public class MyActionFilter : ILazynetServiceActionFilter
    {
        public void OnServiceExecuted(LazynetMessage message)
        {
            LazynetAppManager.GetInstance().Log(message.RouteUrl + " after");
        }

        public void OnServiceExecuting(LazynetMessage message)
        {
            LazynetAppManager.GetInstance().Log(message.RouteUrl + " before");
        }
    }
}
