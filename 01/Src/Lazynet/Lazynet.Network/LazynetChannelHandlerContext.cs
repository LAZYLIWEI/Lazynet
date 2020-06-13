/*
* ==============================================================================
*
* Filename: LazynetChannelHandlerContext
* Description: 
*
* Version: 1.0
* Created: 2020/4/18 19:15:47
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Network
{
    public class LazynetChannelHandlerContext
    {
        public IChannelHandlerContext Context { get; }
        public LazynetSocketType Type { get; }
        public LazynetChannelHandlerContext(IChannelHandlerContext ctx, LazynetSocketType type)
        {
            this.Context = ctx;
            this.Type = type;
        }

        public IChannel Channel
        {
            get
            {
                return Context?.Channel;
            }
        }

        public async void CloseAsync()
        {
            await this.Context.CloseAsync();
        }

        public async void WriteAsync(object message)
        {
            if (this.Type == LazynetSocketType.TcpSocket)
            {
                await this.Context.WriteAsync(message);
            }
            else if (this.Type == LazynetSocketType.Websocket)
            {
                await this.Context.WriteAsync(new TextWebSocketFrame(message.ToString()));
            }
        }

        public async void WriteAndFlushAsync(object message)
        {
            if (this.Type == LazynetSocketType.TcpSocket)
            {
                await this.Context.WriteAndFlushAsync(message);
            }
            else if (this.Type == LazynetSocketType.Websocket)
            {
                await this.Context.WriteAndFlushAsync(new TextWebSocketFrame(message.ToString()));
            }
        }
    }
}
