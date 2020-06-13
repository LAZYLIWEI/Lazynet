/*
* ==============================================================================
*
* Filename: LazynetLogger
* Description: 
*
* Version: 1.0
* Created: 2020/5/3 22:27:24
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

namespace Lazynet.Core.Logger
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public class LazynetLogger : ILazynetLogger
    {
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="content"></param>
        public void Log(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }

            Console.WriteLine($"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}] {content}");
        }


    }
}
