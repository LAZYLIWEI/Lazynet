using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.AppMgrCore
{
    public interface ILazynetStartup
    {
        void Configuration(LazynetAppConfig config);
        void ConfigureFilter(LazynetAppFilter filters);
    }
}
