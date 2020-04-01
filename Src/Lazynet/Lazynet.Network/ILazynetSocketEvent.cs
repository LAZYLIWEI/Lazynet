using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Network
{
    /// <summary>
    /// socket事件
    /// </summary>
    public interface ILazynetSocketEvent
    {
        void Read(IChannelHandlerContext ctx, string msg);
        void Active(IChannelHandlerContext ctx);
        void Inactive(IChannelHandlerContext ctx);
        void ExceptionCaught(IChannelHandlerContext ctx, Exception exception);
    }
}
