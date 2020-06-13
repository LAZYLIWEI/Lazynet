/*
* ==============================================================================
*
* Filename:AppServerHandler
* Description: 
*
* Version: 1.0
* Created: 2020/5/23 0:26:54
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Network;
using Lazynet.Core.Proto;
using Lazynet.Core.Service;
using Lazynet.Core.Timer;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lazynet.LoginApp.AppStart
{
    public class LazynetAppServerHandler : ILazynetSocketEvent
    {
        public LazynetAppContext Context { get; }
        public LazynetAppServerHandler(LazynetAppContext context)
        {
            this.Context = context;
        }

        public void Connect(LazynetHandlerContext ctx)
        {
            CallService(ctx, new LazynetMessage()
            {
                RouteUrl = "/System/Connect",
                Parameters = null
            });
        }

        public void DisConnect(LazynetHandlerContext ctx)
        {
            // 服务器断开链接
            CallService(ctx, new LazynetMessage()
            {
                RouteUrl = "/System/DisConnect",
                Parameters = null
            });
        }

        public void Exception(LazynetHandlerContext ctx, Exception exception)
        {
            this.CallService(ctx, new LazynetMessage()
            {
                RouteUrl = "/System/Exception",
                Parameters = new List<object> {
                    exception
                }
            });
        }

        public void Read(LazynetHandlerContext ctx, string msg)
        {
            // 通过协议解析消息
            if (string.IsNullOrWhiteSpace(msg))
            {
                return;
            }
            var message = SerializeHelper.Deserialize<LazynetFromMessage>(msg);
            this.CallService(ctx, message, message.SessionID);
        }
   
        private void CallService(LazynetHandlerContext ctx, LazynetMessage message, string sessionID = "")
        {
            this.Context.Request = new LazynetServiceRequest() { 
                 Handler = ctx,
                 RouteUrl = message.RouteUrl,
                 SessionID = sessionID
            };
            this.Context.AppFilter.ActionFilter?.OnServiceExecuting(message);
            var result = this.Context.Service.CallService(message);
            if (result != null
                && result.Length > 0
                && !string.IsNullOrEmpty(this.Context.Request.SessionID))
            {
                this.Context.Response(this.Context.Request.SessionID, result[0]);
            }
            this.Context.AppFilter.ActionFilter?.OnServiceExecuted(message);
        }

    }
}
