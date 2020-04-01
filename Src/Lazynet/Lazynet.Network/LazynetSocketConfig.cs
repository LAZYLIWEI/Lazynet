/*
* ==============================================================================
*
* Filename: LazynetSocketConfig
* Description: 
*
* Version: 1.0
* Created: 2020/4/1 21:31:49
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
    public class LazynetSocketConfig
    {
        public int Port { get; set; }
        public int Heartbeat { get; set; }
        public LazynetSocketType Type { get; set; }

        /// <summary>
        /// websocket的路径
        /// websokcet必填
        /// </summary>
        public string Path { get; set; }
    }
}
