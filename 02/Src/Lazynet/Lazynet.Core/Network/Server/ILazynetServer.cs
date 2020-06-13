using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Network.Server
{
    public interface ILazynetServer
    {
        int GetPort();
        ILazynetServer SetSocketEvent(ILazynetSocketEvent socketEvent);
        ILazynetServer SetPort(int port);
        void Bind();
        void Close();
    }
}
