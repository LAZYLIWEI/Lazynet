using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core
{
    public interface ILazynetLogger
    {
        void Info(string serviceID, string str);
    }
}
