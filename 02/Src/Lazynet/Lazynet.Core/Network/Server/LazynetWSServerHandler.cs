/*
* ==============================================================================
*
* Filename:LazynetWSServerHandler
* Description: 
*
* Version: 1.0
* Created: 2020/5/4 0:17:09
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Network.Server
{
    /// <summary>
    /// websocket server handler
    /// </summary>
    public class LazynetWSClientHandler : SimpleChannelInboundHandler<TextWebSocketFrame>
    {
        public ILazynetSocketEvent SocketEvent { get; }
        public LazynetWSClientHandler(ILazynetSocketEvent socketEvent)
        {
            this.SocketEvent = socketEvent;
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, TextWebSocketFrame msg)
        {
            this.SocketEvent?.Read(new LazynetHandlerContext(ctx, LazynetSocketType.Websocket), msg.Text());
        }

        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            this.SocketEvent?.Connect(new LazynetHandlerContext(ctx, LazynetSocketType.Websocket));
            base.ChannelActive(ctx);
        }

        public override void UserEventTriggered(IChannelHandlerContext ctx, object evt)
        {
            if (evt is IdleStateEvent)
            {
                ctx.Channel.CloseAsync();
            }
        }

        public override void ChannelInactive(IChannelHandlerContext ctx)
        {
            this.SocketEvent?.DisConnect(new LazynetHandlerContext(ctx, LazynetSocketType.Websocket));
            base.ChannelInactive(ctx);
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
        {
            this.SocketEvent?.Exception(new LazynetHandlerContext(ctx, LazynetSocketType.Websocket), exception);
            ctx.CloseAsync();
        }

    }
}
