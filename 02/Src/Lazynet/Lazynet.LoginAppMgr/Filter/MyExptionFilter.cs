/*
* ==============================================================================
*
* Filename: MyExptionFilter
* Description: 
*
* Version: 1.0
* Created: 2020/5/30 22:08:19
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.AppMgrCore;
using Lazynet.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginAppMgr.Filter
{
    public class MyExptionFilter : ILazynetServiceExceptionFilter
    {
        public void OnException(Exception ex)
        {
            LazynetAppManager.GetInstance().Log(ex.ToString(), LazynetLogLevel.Error);
        }
    }
}
