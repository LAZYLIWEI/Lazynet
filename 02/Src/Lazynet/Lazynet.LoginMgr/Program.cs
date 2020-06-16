using Lazynet.AppMgrCore;
using Lazynet.Core.Cache;
using Lazynet.Core.Logger;
using Lazynet.Core.Network.Server;
using System;

namespace Lazynet.LoginMgr
{
    class Program
    {
        static void Main(string[] args)
        {
            LazynetAppManager
                .GetInstance()
                .UseStartup<Startup>()
                .Builder()
                .Start();
            Console.ReadKey();
        }
    }
}
