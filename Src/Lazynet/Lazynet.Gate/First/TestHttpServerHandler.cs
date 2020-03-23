using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Gate.First
{
    public class TestHttpServerHandler : SimpleChannelInboundHandler<IHttpObject>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, IHttpObject msg)
        {
            if (msg is IHttpRequest)
            {
                Console.WriteLine("ChannelRead0");
                IByteBuffer content = Unpooled.CopiedBuffer("hello world", Encoding.UTF8);
                IFullHttpResponse response = new DefaultFullHttpResponse(HttpVersion.Http11, HttpResponseStatus.OK, content);
                response.Headers.Set(HttpHeaderNames.ContentType, "text/plain");
                response.Headers.Set(HttpHeaderNames.ContentLength, content.ReadableBytes);
                ctx.WriteAndFlushAsync(response);

                ctx.CloseAsync();
            }
        }

        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            Console.WriteLine("active");
            base.ChannelActive(ctx);
        }


        public override void HandlerAdded(IChannelHandlerContext cxt)
        {
            Console.WriteLine("added");
            base.HandlerAdded(cxt);
        }


        public override void ChannelRegistered(IChannelHandlerContext ctx)
        {
            Console.WriteLine("registered");
            base.ChannelRegistered(ctx);
        }


        public override void ChannelUnregistered(IChannelHandlerContext context)
        {
            Console.WriteLine("unregistered");
            base.ChannelUnregistered(context);
        }


        public override void ChannelInactive(IChannelHandlerContext ctx)
        {
            Console.WriteLine("inactive");
            base.ChannelInactive(ctx);
        }
    }
}
