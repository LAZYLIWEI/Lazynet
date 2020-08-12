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
using Lazynet.Core.Logger;
using Lazynet.Core.Network;
using Lazynet.Core.Proto;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lazynet.GameApp
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
            this.CallService(ctx, new LazynetMessage()
            {
                RouteUrl = "/System/Connect",
                Parameters = null
            });
        }

        public void DisConnect(LazynetHandlerContext ctx)
        {
            this.CallService(ctx, new LazynetMessage()
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
            this.Context.Log(LazynetLogLevel.Debug, "===recv data is " + msg);
            var message = SerializeHelper.Deserialize<LazynetMessage>(msg);
            this.CallService(ctx, message);
        }

        private void CallService(LazynetHandlerContext ctx, LazynetMessage message)
        {
            try
            {
                this.Context.CurrentContext = ctx;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                this.Context.ActionProxy.Call(message);
                sw.Stop();
                if (sw.Elapsed.TotalSeconds >= 3)
                {
                    this.Context.Log(LazynetLogLevel.Warn, message.RouteUrl + " action run slow ");
                }

            }
            catch (Exception ex)
            {
                this.Context.Log(LazynetLogLevel.Error, ex.ToString());
            }
        }

    }
}
