﻿using Lazynet.Core.LUA;
using Lazynet.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.AppCore
{
    public interface ILazynetStartup
    {
        void Configuration(LazynetAppConfig config);
        void ConfigureServices(LazynetAppService appService);
        void ConfigureFilter(LazynetAppFilter filters);
        void ConfigureLua(ILazynetLua lua);

        void StartBefore();
        void StartAfter();
    }
}
