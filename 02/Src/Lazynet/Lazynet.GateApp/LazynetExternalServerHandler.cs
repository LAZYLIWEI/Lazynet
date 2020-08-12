/*
* ==============================================================================
*
* Filename: ExternalServerSocketEvent
* Description: 
*
* Version: 1.0
* Created: 2020/5/21 23:49:36
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Network;
using Lazynet.Core.Proto;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;

namespace Lazynet.GateApp
{
    /// <summary>
    /// 外部服务器处理
    /// </summary>
    public class LazynetExternalServerHandler : ILazynetSocketEvent
    {
        public LazynetAppContext Context { get; }
        public LazynetExternalServerHandler(LazynetAppContext context)
        {
            this.Context = context;
        }

        public void Connect(LazynetHandlerContext ctx)
        {
            this.Context.CallAction(new LazynetMessage()
            {
                RouteUrl = "/ExternalServer/Connect",
                Parameters = new List<object>() {
                    ctx
                }
            });
        }

        public void DisConnect(LazynetHandlerContext ctx)
        {
            this.Context.CallAction(new LazynetMessage()
            {
                RouteUrl = "/ExternalServer/DisConnect",
                Parameters = new List<object>() {
                    ctx
                }
            });
        }

        public void Exception(LazynetHandlerContext ctx, Exception ex)
        {
            this.Context.CallAction(new LazynetMessage()
            {
                RouteUrl = "/ExternalServer/Exception",
                Parameters = new List<object>() {
                    ctx,
                    ex.ToString()
                }
            });
        }

        public void Read(LazynetHandlerContext ctx, string msg)
        {
            var sessionMessage = SerializeHelper.Deserialize<LazynetSessionMessage>(msg);
            var message = new LazynetMessage();
            message.RouteUrl = "/ExternalServer/Read";
            message.Parameters = new List<object>() {
                ctx,
                sessionMessage.ServerName,
                sessionMessage.RouteUrl,
            };
            message.Parameters.AddRange(sessionMessage.Parameters);
            this.Context.CallAction(message);

        }

    }
}
