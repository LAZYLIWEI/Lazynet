/*
* ==============================================================================
*
* Filename: LazynetLogger
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 0:47:24
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
    /// 日志
    /// </summary>
    public class LazynetLogger : ILazynetLogger
    {
        public void Info(string serviceID, string str)
        {
            Console.WriteLine($"[{serviceID}] [{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}] {str}");
        }
    }
}
