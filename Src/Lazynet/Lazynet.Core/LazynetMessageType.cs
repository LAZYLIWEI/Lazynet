/*
* ==============================================================================
*
* Filename: LazynetMessageType
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 1:21:38
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
    /// 消息类型
    /// </summary>
    public enum LazynetMessageType
    {
        /// <summary>
        /// 系统消息
        /// </summary>
        System,
        /// <summary>
        /// lua消息
        /// </summary>
        Lua,
        /// <summary>
        /// c#消息
        /// </summary>
        Sharp,
        /// <summary>
        /// socket消息
        /// </summary>
        Socket,
    }
}
