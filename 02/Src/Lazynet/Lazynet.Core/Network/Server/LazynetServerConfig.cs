/*
* ==============================================================================
*
* Filename: LazynetServerConfig
* Description: 
*
* Version: 1.0
* Created: 2020/5/3 23:57:26
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Network.Server
{
    /// <summary>
    /// 服务配置
    /// </summary>
    public class LazynetServerConfig
    {
        /// <summary>
        /// 绑定的端口号
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

        /// <summary>
        /// 心跳
        /// </summary>
        public int Heartbeat { get; set; }


        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public bool IsValid(out string errorMsg)
        {
            errorMsg = string.Empty;
            if (!VerificationHelper.Range(this.Port, 0, 65535))
            {
                errorMsg = "端口号无效";
                return false;
            }
            if (this.SocketType == LazynetSocketType.Websocket
                && string.IsNullOrWhiteSpace(this.WebsocketPath))
            {
                errorMsg = "使用websocket时,WebsocketPath必填";
                return false;
            }

            return true;
        }

    }
}
