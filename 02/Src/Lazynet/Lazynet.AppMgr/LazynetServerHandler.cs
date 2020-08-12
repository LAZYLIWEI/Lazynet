/*
* ==============================================================================
*
* Filename: ExternalServerHandler
* Description: 
*
* Version: 1.0
* Created: 2020/5/21 23:58:11
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
using System.Text;

namespace Lazynet.AppMgr
{
    /// <summary>
    /// 服务器处理
    /// </summary>
    public class LazynetServerHandler : ILazynetSocketEvent
    {
        public LazynetAppContext Context { get; }

        public LazynetServerHandler(LazynetAppContext context)
        {
            this.Context = context;
        }

        public void Connect(LazynetHandlerContext ctx)
        {
            this.Context.Call(new LazynetMessage()
            {
                RouteUrl = LazynetActionConstant.NodeConnect,
                Parameters = new List<object>()
                {
                    ctx,
                }
            });
        }

        public void DisConnect(LazynetHandlerContext ctx)
        {
            this.Context.Call(new LazynetMessage()
            {
                RouteUrl = LazynetActionConstant.NodeDisconnect,
                Parameters = new List<object>()
                {
                    ctx
                }
            });
        }

        public void Exception(LazynetHandlerContext ctx, Exception exception)
        {
            this.Context.Call(new LazynetMessage()
            {
                RouteUrl = LazynetActionConstant.NodeException,
                Parameters = new List<object>() {
                    ctx,
                    exception.ToString()
                }
            });
        }

        public void Read(LazynetHandlerContext ctx, string msg)
        {
            this.Context.Log(LazynetLogLevel.Debug, "===recv data is " + msg);
            var message = SerializeHelper.Deserialize<LazynetMessage>(msg);
            message.Parameters.Insert(0, ctx);
            this.Context.Call(message);
        }

    }

}
