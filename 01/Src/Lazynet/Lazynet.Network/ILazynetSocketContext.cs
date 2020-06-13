using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Network
{
    public interface ILazynetSocketContext
    {
        ILazynetSocketEvent Event { get; }
        LazynetSocketConfig Config { get; }
        LazynetSocket SetEvent(ILazynetSocketEvent ev);
        void BindAsync();
        void Close();
    }
}
