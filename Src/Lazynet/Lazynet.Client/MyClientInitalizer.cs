using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Client
{
    public class MyClientInitalizer : ChannelInitializer<IChannel>
    {
        protected override void InitChannel(IChannel channel)
        {
            var pipeline = channel.Pipeline;
            pipeline.AddLast(new LengthFieldBasedFrameDecoder(int.MaxValue, 0, 4, 0, 4));
            pipeline.AddLast(new LengthFieldPrepender(4));
            pipeline.AddLast(new StringDecoder(Encoding.UTF8));
            pipeline.AddLast(new StringEncoder(Encoding.UTF8));
            pipeline.AddLast(new MyClientHandler());
        }
    }
}
