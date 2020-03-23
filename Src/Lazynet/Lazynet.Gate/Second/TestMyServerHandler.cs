using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Gate.Second
{
    public class TestMyServerHandler : SimpleChannelInboundHandler<string>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, string msg)
        {
            Console.WriteLine(ctx.Channel.RemoteAddress);
            Console.WriteLine("server output: " + msg);
            ctx.WriteAndFlushAsync("from server: " + Guid.NewGuid());
        }


        public override void ChannelActive(IChannelHandlerContext context)
        {
            Console.WriteLine($"客户端{context.Channel.RemoteAddress}在线.");
            base.ChannelActive(context);
        }

        //服务器监听到客户端不活动时
        public override void ChannelInactive(IChannelHandlerContext context)
        {
            Console.WriteLine($"客户端{context.Channel.RemoteAddress}离线了.");
            base.ChannelInactive(context);
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
        {
            Console.WriteLine(exception.ToString());
            ctx.CloseAsync();
        }

    }
}
