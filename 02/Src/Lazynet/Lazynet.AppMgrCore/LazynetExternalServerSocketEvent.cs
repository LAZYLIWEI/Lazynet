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
using Lazynet.Core.Service;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.AppMgrCore
{
    /// <summary>
    /// 内部服务器处理
    /// </summary>
    public class LazynetExternalServerSocketEvent : ILazynetSocketEvent
    {
        public LazynetExternalServerContext ServerContext { get; }
        public LazynetAppContext Context { get; }

        public LazynetExternalServerSocketEvent(LazynetExternalServerContext serverContext, LazynetAppContext context)
        {
            this.ServerContext = serverContext;
            this.Context = context;
        }

        public void Connect(LazynetHandlerContext ctx)
        {
            this.CallService(new LazynetMessage()
            {
                RouteUrl = LazynetActionConstant.NodeVisit,
                Parameters = new List<object>() {
                    ctx
                } 
            });
        }

        public void DisConnect(LazynetHandlerContext ctx)
        {
            this.CallService(new LazynetMessage()
            {
                RouteUrl = LazynetActionConstant.NodeDisconnect,
                Parameters = new List<object>() {
                    ctx
                }
            });
        }

        public void Exception(LazynetHandlerContext ctx, Exception exception)
        {
            this.CallService(new LazynetMessage()
            {
                RouteUrl = LazynetActionConstant.NodeException,
                Parameters = new List<object>() {
                    ctx,
                    exception
                }
            });
        }

        public void Read(LazynetHandlerContext ctx, string msg)
        {
            // 通过协议解析消息
            var message = SerializeHelper.Deserialize<LazynetMessage>(msg);
            if (message.Parameters == null)
            {
                message.Parameters = new List<object>();
            }
            message.Parameters.Insert(0, ctx);
            CallService(message, msg);
        }

        protected void CallService(LazynetMessage message, string msg = "")
        {
            if (message != null)
            {
                if (this.ServerContext.ServiceDict.ContainsKey(message.RouteUrl))
                {
                    var trigger = this.ServerContext.ServiceDict[message.RouteUrl];
                    trigger.CallBack(new LazynetServiceEntity()
                    {
                        Type = LazynetServiceType.Normal,
                        RouteUrl = message.RouteUrl,
                        Parameters = message.Parameters?.ToArray()
                    });
                }
                else
                {
                    this.Context.Logger.Warn(string.Format("don't have the trigger , routeUrl is {0}", message.RouteUrl));
                }
            }
            else
            {
                this.Context.Logger.Warn(string.Format("message deserialize failed, msg is {0}", msg));
            }
        }


    }
}
