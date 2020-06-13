/*
* ==============================================================================
*
* Filename: LazynetClientConfig
* Description: 
*
* Version: 1.0
* Created: 2020/5/4 0:49:32
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

namespace Lazynet.Core.Network.Client
{
    public class LazynetClientConfig
    {
        /// <summary>
        /// 连接服务器的ip
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 连接服务器的端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// socket类型
        /// </summary>
        public LazynetSocketType SocketType { get; set; }

        /// <summary>
        /// websocket需要配置路径,tcpsocket下可以不配置
        /// </summary>
        public string WebsocketPath { get; set; }
    }
}
