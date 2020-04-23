/*
* ==============================================================================
*
* Filename: LazynetDefaultSocketEvent
* Description: 
*
* Version: 1.0
* Created: 2020/4/2 1:07:44
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using DotNetty.Transport.Channels;
using Lazynet.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core
{
    public class LazynetDefaultSocketEvent : ILazynetSocketEvent
    {
        public ILazynetService ServiceContext { get; }
        public LazynetDefaultSocketEvent(ILazynetService serviceContext)
        {
            this.ServiceContext = serviceContext;
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="ctx"></param>
        public void Active(LazynetChannelHandlerContext ctx)
        {
            var parameters = new object[] {
                ctx,
                ctx.Channel.RemoteAddress.ToString()
            };
            var serviceMessage = new LazynetServiceMessage(LazynetMessageType.Normal, ServiceContext.SocketEvent.ActiveEvent, parameters);
            ServiceContext.Context.RecvMessage(ServiceContext.ID, serviceMessage);
        }

        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="exception"></param>
        public void ExceptionCaught(LazynetChannelHandlerContext ctx, Exception exception)
        {
            var parameters = new object[] {
                ctx,
                ctx.Channel.RemoteAddress.ToString()
            };
            var serviceMessage = new LazynetServiceMessage(LazynetMessageType.Normal, ServiceContext.SocketEvent.ExceptionEvent,  parameters);
            ServiceContext.Context.RecvMessage(ServiceContext.ID, serviceMessage);
        }

        /// <summary>
        /// 断线
        /// </summary>
        /// <param name="ctx"></param>
        public void Inactive(LazynetChannelHandlerContext ctx)
        {
            var parameters = new object[] {
                ctx,
                ctx.Channel.RemoteAddress.ToString()
            };
            var serviceMessage = new LazynetServiceMessage(LazynetMessageType.Normal, ServiceContext.SocketEvent.InactiveEvent, parameters);
            ServiceContext.Context.RecvMessage(ServiceContext.ID, serviceMessage);
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="msg"></param>
        public void Read(LazynetChannelHandlerContext ctx, string msg)
        {
            var parameters = new object[] {
                ctx,
                ctx.Channel.RemoteAddress.ToString(),
                msg
            };
            var serviceMessage = new LazynetServiceMessage(LazynetMessageType.Normal, ServiceContext.SocketEvent.ReadEvent, parameters);
            ServiceContext.Context.RecvMessage(ServiceContext.ID, serviceMessage);
        }
    }
}
