/*
* ==============================================================================
*
* Filename:RedisMgr
* Description: 
*
* Version: 1.0
* Created: 2020/5/27 0:33:52
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Cache;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginMgr
{

    /// <summary>
    /// redis manager
    /// </summary>
    public class RedisMgr
    {
        private static LazynetRedis redis = null;

        static RedisMgr()
        {
            redis = new LazynetRedis("47.92.213.250", "8g199696QQ"); ;
        }

        public static LazynetRedis GetInstance()
        {
            return redis;
        }

    }
}
