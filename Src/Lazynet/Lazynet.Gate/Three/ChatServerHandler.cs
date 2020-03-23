using DotNetty.Common.Concurrency;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Groups;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Gate.Three
{
    public class ChatServerHandler : SimpleChannelInboundHandler<string>
    {

        private static IChannelGroup ChannelGroup { get;}

        static ChatServerHandler()
        {
            ChatServerHandler.ChannelGroup = new DefaultChannelGroup(new SingleThreadEventExecutor("ChannelGroup", TimeSpan.Zero));
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, string msg)
        {
            var channel = ctx.Channel;
            foreach (var item in ChatServerHandler.ChannelGroup)
            {
                if (channel != item)
                {
                    item.WriteAndFlushAsync("[客户]" + channel.RemoteAddress + "发送了消息--" + msg + "\n");
                }
                else
                {
                    item.WriteAndFlushAsync("[自己]" + "发送了消息" + msg + "\n");
                }
            }
        }

        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            var channel = ctx.Channel;
            Console.WriteLine(channel.RemoteAddress + "上线啦!!!");
        } 

        public override void HandlerAdded(IChannelHandlerContext ctx)
        {
            ChannelGroup.WriteAndFlushAsync("[客户端]-" + ctx.Channel.RemoteAddress + "加入\n");
            ChannelGroup.Add(ctx.Channel);
        }

        public override void HandlerRemoved(IChannelHandlerContext ctx)
        {
            var channel = ctx.Channel;
            Console.WriteLine(channel.RemoteAddress + "离线啦!!!");
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
        {
            Console.WriteLine(exception.ToString());
            ctx.CloseAsync();
        }
    }
}
