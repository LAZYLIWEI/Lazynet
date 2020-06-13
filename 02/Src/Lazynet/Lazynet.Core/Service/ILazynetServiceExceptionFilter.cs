using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Service
{
    public interface ILazynetServiceExceptionFilter
    {
        void OnException(Exception ex);
    }
}
