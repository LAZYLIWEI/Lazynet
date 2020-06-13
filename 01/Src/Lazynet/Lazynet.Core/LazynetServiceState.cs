/*
* ==============================================================================
*
* Filename: LazynetServiceState
* Description: 
*
* Version: 1.0
* Created: 2020/3/22 22:33:38
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
    public enum LazynetServiceState
    {
        /// <summary>
        /// 未启动
        /// </summary>
        UnStart,
        /// <summary>
        /// 启动
        /// </summary>
        Start,
        /// <summary>
        /// 运行
        /// </summary>
        Runing,
        /// <summary>
        /// 挂起
        /// </summary>
        Suspend,
        /// <summary>
        /// 继续
        /// </summary>
        Resume,
        /// <summary>
        /// 退出
        /// </summary>
        Exit
    }
}
