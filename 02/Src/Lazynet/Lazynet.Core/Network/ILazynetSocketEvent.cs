using Lazynet.Core.Network.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Network
{
    public interface ILazynetSocketEvent
    {
        void Read(LazynetHandlerContext ctx, string msg);
        void Connect(LazynetHandlerContext ctx);
        void DisConnect(LazynetHandlerContext ctx);
        void Exception(LazynetHandlerContext ctx, Exception exception);
    }
}
