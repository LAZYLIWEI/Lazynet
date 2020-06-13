/*
* ==============================================================================
*
* Filename: LoggerMgr
* Description: 
*
* Version: 1.0
* Created: 2020/5/22 0:07:15
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginMgr
{
    public class LoggerMgr
    {
        private static ILazynetLogger logger = null;

        static LoggerMgr()
        {
            logger = new LazynetLogger();
        }

        public static ILazynetLogger GetInstance()
        {
            return logger;
        }
    }
}
