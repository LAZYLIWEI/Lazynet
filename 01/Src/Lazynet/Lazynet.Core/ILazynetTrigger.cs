using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core
{
    public interface ILazynetTrigger
    {
        object[] CallBack(LazynetServiceMessage message);
    }
}
