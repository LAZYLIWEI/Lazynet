/*
* ==============================================================================
*
* Filename: LazynetHandlerContext
* Description: 
*
* Version: 1.0
* Created: 2020/5/4 0:26:29
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

namespace Lazynet.Core.Network
{
    /// <summary>
    /// handler
    /// </summary>
    public class LazynetHandlerContext
    {
        public IChannelHandlerContext Context { get; }
        public LazynetSocketType SocketType { get; }

        public LazynetHandlerContext(
                IChannelHandlerContext context,
                LazynetSocketType socketType)
        {
            this.Context = context;
            this.SocketType = socketType;
        }

        public string GetAddress()
        {
            return this.Context.Channel.RemoteAddress.ToString();
        }

        public async void CloseAsync()
        {
            await this.Context.CloseAsync();
        }

        public async void WriteAsync(object message)
        {
            if (this.SocketType == LazynetSocketType.TcpSocket)
            {
                await this.Context.WriteAsync(message);
            }
            else if (this.SocketType == LazynetSocketType.Websocket)
            {
                await this.Context.WriteAsync(new TextWebSocketFrame(message.ToString()));
            }
        }

        public async void WriteAndFlushAsync(object message)
        {

            if (this.SocketType == LazynetSocketType.TcpSocket)
            {
                await this.Context.WriteAndFlushAsync(message);
            }
            else if (this.SocketType == LazynetSocketType.Websocket)
            {
                await this.Context.WriteAndFlushAsync(new TextWebSocketFrame(message.ToString()));
            }
        }

    }
}
