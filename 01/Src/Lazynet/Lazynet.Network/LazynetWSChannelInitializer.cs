/*
* ==============================================================================
*
* Filename: WebSocketChannelInitializer
* Description: 
*
* Version: 1.0
* Created: 2020/4/1 21:42:09
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Handlers.Streams;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Network
{
    /// <summary>
    /// websocket初始化
    /// </summary>
    public class LazynetWSChannelInitializer : ChannelInitializer<IChannel>
    {
        public ILazynetSocketContext Context { get; }
        public LazynetWSChannelInitializer(ILazynetSocketContext context)
        {
            this.Context = context;
        }

        protected override void InitChannel(IChannel channel)
        {
            IChannelPipeline pipeline = channel.Pipeline;
            pipeline.AddLast(new IdleStateHandler(Context.Config.Heartbeat, 0, 0));
            pipeline.AddLast(new HttpServerCodec());
            pipeline.AddLast(new ChunkedWriteHandler<byte>());
            pipeline.AddLast(new HttpObjectAggregator(8192));
            pipeline.AddLast(new WebSocketServerProtocolHandler(Context.Config.WSPath));
            pipeline.AddLast(new LazynetWSChannelHandler(Context));
        }

    }
}
