using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class MyClientHandler : SimpleChannelInboundHandler<string>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, string msg)
        {
            Console.WriteLine(ctx.Channel.RemoteAddress);
            Console.WriteLine("client output: " + msg);
            ctx.WriteAndFlushAsync("from client: " + DateTime.Now);
        }


        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            Console.WriteLine("active");
            ctx.WriteAndFlushAsync("来自客户端的问候");
        }



        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
        {
            Console.WriteLine(exception.ToString());
            ctx.CloseAsync();
        }
    }
}
