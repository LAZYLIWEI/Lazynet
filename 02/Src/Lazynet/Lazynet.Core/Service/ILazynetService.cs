using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Service
{
    public interface ILazynetService
    {
        LazynetServiceType Type { get; }
        object[] CallBack(LazynetServiceEntity message);
    }
}
