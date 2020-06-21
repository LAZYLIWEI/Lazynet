/*
* ==============================================================================
*
* Filename: LazynetActionConstant
* Description: 
*
* Version: 1.0
* Created: 2020/6/22 0:12:30
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

namespace Lazynet.AppMgrCore
{
    public class LazynetActionConstant
    {
        public static readonly string SessionConnect = "/Session/Connect";
        public static readonly string SessionDisconnect = "/Session/DisConnect";

        public static readonly string NodeVisit = "Visit";
        public static readonly string NodeDisconnect = "DisConnect";
        public static readonly string NodeException = "Exception";
    }
}
