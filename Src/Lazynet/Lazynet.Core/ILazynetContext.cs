using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core
{
    public interface ILazynetContext
    {
        void SendMessage(int serviceID, LazynetServiceMessage serviceMessage);
        void RemoveService(int serviceID);
        public LazynetRoute Route { get; }
    }
}
