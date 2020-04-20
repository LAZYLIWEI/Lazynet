using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core
{
    public interface ILazynetContext
    {
        ILazynetService GetService(int serviceID);
        void RecvMessage(int serviceID, LazynetServiceMessage serviceMessage);
        void RemoveService(int serviceID);
        LazynetLuaService CreateLuaService(string filename);
        int GetServiceID(string alias);
        int GetGlobaServiceID();
        ILazynetLogger Logger { get; }
    }
}
