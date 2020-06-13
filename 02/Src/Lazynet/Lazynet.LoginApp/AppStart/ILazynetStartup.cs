using Lazynet.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginApp.AppStart
{
    public interface ILazynetStartup
    {
        void Configuration(LazynetAppConfig config);
        void ConfigureServices(LazynetAppService appService);
        void ConfigureFilter(LazynetAppFilter filters);
    }
}
