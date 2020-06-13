using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Network.Client
{
    public interface ILazynetClient
    {
        ILazynetClient SetIP(string ip);
        ILazynetClient SetPort(int port);
        ILazynetClient SetSocketEvent(ILazynetSocketEvent socketEvent);
        bool ConnectToHost();
        bool TryConnectToHost(int time, int interval, Action<string> exception);
        bool WaitConnectToHost(int interval, Action<string> exception);
    }
}
