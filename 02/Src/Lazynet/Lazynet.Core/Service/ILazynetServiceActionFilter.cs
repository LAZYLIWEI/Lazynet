using Lazynet.Core.Proto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Service
{
    public interface ILazynetServiceActionFilter
    {
        void OnServiceExecuted(LazynetMessage message);
        void OnServiceExecuting(LazynetMessage message);
    }
}
