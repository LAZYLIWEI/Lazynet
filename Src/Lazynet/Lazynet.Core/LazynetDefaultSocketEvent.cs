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

        public void Active(IChannelHandlerContext ctx)
        {
            ServiceContext.Context.RecvMessage(ServiceContext.ID, new LazynetServiceMessage(LazynetMessageType.Socket, "active", new object[] { ctx.Channel.RemoteAddress.ToString() }));
        }

        public void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
        {
            ServiceContext.Context.RecvMessage(ServiceContext.ID, new LazynetServiceMessage(LazynetMessageType.Socket, "exception", new object[] { ctx.Channel.RemoteAddress.ToString() }));
        }

        public void Inactive(IChannelHandlerContext ctx)
        {
            ServiceContext.Context.RecvMessage(ServiceContext.ID, new LazynetServiceMessage(LazynetMessageType.Socket, "inactive", new object[] { ctx.Channel.RemoteAddress.ToString() }));
        }

        public void Read(IChannelHandlerContext ctx, string msg)
        {
            ServiceContext.Context.RecvMessage(ServiceContext.ID, new LazynetServiceMessage(LazynetMessageType.Socket, "read", new object[] { ctx.Channel.RemoteAddress.ToString() }));
        }
    }
}
