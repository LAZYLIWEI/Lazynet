/*
* ==============================================================================
*
* Filename: LazynetTSChannelInitializer
* Description: 
*
* Version: 1.0
* Created: 2020/4/1 21:52:18
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using DotNetty.Codecs;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using Lazynet.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Network
{
    /// <summary>
    /// tcp socket
    /// </summary>
    public class LazynetTSChannelInitializer : ChannelInitializer<IChannel>
    {
        public ILazynetSocket Context { get; }
        public LazynetTSChannelInitializer(ILazynetSocket context)
        {
            this.Context = context;
        }

        protected override void InitChannel(IChannel channel)
        {
            var pipeline = channel.Pipeline;
            pipeline.AddLast(new IdleStateHandler(Context.Config.Heartbeat, 0, 0));
            pipeline.AddLast(new LengthFieldBasedFrameDecoder(int.MaxValue, 0, 4, 0, 4));
            pipeline.AddLast(new LengthFieldPrepender(4));
            pipeline.AddLast(new StringDecoder(Encoding.UTF8));
            pipeline.AddLast(new StringEncoder(Encoding.UTF8));
            pipeline.AddLast(new LazynetTSChannelHandler(Context));
        }
    }
}
