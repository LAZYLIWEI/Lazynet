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

namespace Lazynet.LoginMgr
{
    /// <summary>
    /// 内部服务器处理
    /// </summary>
    public class ExternalServerSocketEvent : ILazynetSocketEvent
    {
        public Dictionary<string, ILazynetService> ServiceDict { get; }

        public ExternalServerSocketEvent(Dictionary<string, ILazynetService> serviceDict)
        {
            this.ServiceDict = serviceDict;
        }

        public void Connect(LazynetHandlerContext ctx)
        {
            this.CallService(new LazynetMessage()
            {
                RouteUrl = "Visit",
                Parameters = new List<object>() {
                    ctx
                } 
            });
        }

        public void DisConnect(LazynetHandlerContext ctx)
        {
            this.CallService(new LazynetMessage()
            {
                RouteUrl = "DisConnect",
                Parameters = new List<object>() {
                    ctx
                }
            });
        }

        public void Exception(LazynetHandlerContext ctx, Exception exception)
        {
            this.CallService(new LazynetMessage()
            {
                RouteUrl = "Exception",
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
                if (this.ServiceDict.ContainsKey(message.RouteUrl))
                {
                    var trigger = this.ServiceDict[message.RouteUrl];
                    trigger.CallBack(new LazynetServiceEntity()
                    {
                        Type = LazynetServiceType.Normal,
                        RouteUrl = message.RouteUrl,
                        Parameters = message.Parameters?.ToArray()
                    });
                }
                else
                {
                    LoggerMgr.GetInstance().Log(string.Format("don't have the trigger , routeUrl is {0}", message.RouteUrl));
                }
            }
            else
            {
                LoggerMgr.GetInstance().Log(string.Format("message deserialize failed, msg is {0}", msg));
            }
        }


    }
}
