/*
* ==============================================================================
*
* Filename: InteriorServerSocketEvent
* Description: 
*
* Version: 1.0
* Created: 2020/5/21 23:45:55
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
using System.Diagnostics;
using System.Linq;

namespace Lazynet.GateApp
{
    public class LazynetInteriorServerHandler : ILazynetSocketEvent
    {
        public LazynetAppContext Context { get; }

        public LazynetInteriorServerHandler(LazynetAppContext context)
        {
            this.Context = context;
        }

        public void Connect(LazynetHandlerContext ctx)
        {
            this.Context.CallAction(new LazynetMessage()
            {
                RouteUrl = "/InteriorServer/Connect",
                Parameters = new List<object>()
                {
                    ctx
                }
            });
        }

        public void DisConnect(LazynetHandlerContext ctx)
        {
            this.Context.CallAction(new LazynetMessage()
            {
                RouteUrl = "/InteriorServer/DisConnect",
                Parameters = new List<object>()
                {
                    ctx
                }
            });
        }

        public void Exception(LazynetHandlerContext ctx, Exception ex)
        {
            this.Context.CallAction(new LazynetMessage()
            {
                RouteUrl = "/InteriorServer/Exception",
                Parameters = new List<object>()
                {
                    ctx
                }
            });
        }

        public void Read(LazynetHandlerContext ctx, string msg)
        {
            // 转发消息
            var message = SerializeHelper.Deserialize<LazynetMessage>(msg);
            var messageWrapper = new LazynetMessage();
            messageWrapper.RouteUrl = "/InteriorServer/Read";
            messageWrapper.Parameters = new List<object>();
            messageWrapper.Parameters.Add(ctx);
            messageWrapper.Parameters.Add(message.RouteUrl);
            foreach (var item in message.Parameters)
            {
                messageWrapper.Parameters.Add(item);
            }
            this.Context.CallAction(messageWrapper);
        }


    }
}
