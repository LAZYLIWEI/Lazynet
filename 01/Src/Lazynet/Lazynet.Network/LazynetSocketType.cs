/*
* ==============================================================================
*
* Filename: LazynetSocketType
* Description: 
*
* Version: 1.0
* Created: 2020/4/1 23:43:56
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

namespace Lazynet.Network
{
    /// <summary>
    /// socket类型
    /// </summary>
    public enum LazynetSocketType
    {
        /// <summary>
        /// tcp
        /// </summary>
        TcpSocket,
        /// <summary>
        /// websocket
        /// </summary>
        Websocket,
    }
}
