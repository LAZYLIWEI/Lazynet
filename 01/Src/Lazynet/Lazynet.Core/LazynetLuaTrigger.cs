/*
* ==============================================================================
*
* Filename: LazynetTriggerEntity
* Description: 
*
* Version: 1.0
* Created: 2020/3/29 11:25:24
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using LuaInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core
{
    public class LazynetLuaTrigger : ILazynetTrigger
    {
        /// <summary>
        /// 命令
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// 函数
        /// </summary>
        public LuaFunction Function { get; set; }

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public object[] CallBack(LazynetServiceMessage message)
        {
            var result = this.Function?.Call(message.Parameters);
            return result;
        }
    }
}
