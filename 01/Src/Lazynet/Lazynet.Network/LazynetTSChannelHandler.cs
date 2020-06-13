/*
* ==============================================================================
*
* Filename: LazynetWSChaneelHandler
* Description: 
*
* Version: 1.0
* Created: 2020/4/1 23:39:32
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Network
{
    public  class LazynetTSChannelHandler : SimpleChannelInboundHandler<string>
    {
        public ILazynetSocketContext Context { get; }
        public LazynetTSChannelHandler(ILazynetSocketContext context)
        {
            this.Context = context;
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, string msg)
        {
            Context.Event?.Read(new LazynetChannelHandlerContext(ctx, this.Context.Config.Type), msg);
        }

        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            Context.Event?.Active(new LazynetChannelHandlerContext(ctx, this.Context.Config.Type));
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
            Context.Event?.Inactive(new LazynetChannelHandlerContext(ctx, this.Context.Config.Type));
            base.ChannelInactive(ctx);
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
        {
            Context.Event?.ExceptionCaught(new LazynetChannelHandlerContext(ctx, this.Context.Config.Type), exception);
            ctx.CloseAsync();
        }

    }
}
