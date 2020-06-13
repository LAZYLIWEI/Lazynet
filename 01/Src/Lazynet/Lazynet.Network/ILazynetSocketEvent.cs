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
        void Read(LazynetChannelHandlerContext ctx, string msg);
        void Active(LazynetChannelHandlerContext ctx);
        void Inactive(LazynetChannelHandlerContext ctx);
        void ExceptionCaught(LazynetChannelHandlerContext ctx, Exception exception);
    }
}
