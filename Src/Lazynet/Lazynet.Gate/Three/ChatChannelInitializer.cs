using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Gate.Three
{
    public class ChatChannelInitializer : ChannelInitializer<IChannel>
    {
        protected override void InitChannel(IChannel channel)
        {
            var pipeline = channel.Pipeline;
            pipeline.AddLast(new DelimiterBasedFrameDecoder(4096, Delimiters.LineDelimiter()));
            pipeline.AddLast(new StringDecoder(Encoding.UTF8));
            pipeline.AddLast(new StringEncoder(Encoding.UTF8));
            pipeline.AddLast(new ChatServerHandler());
        }
    }
}
