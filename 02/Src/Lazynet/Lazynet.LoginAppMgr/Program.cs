using Lazynet.AppMgrCore;
using System;

namespace Lazynet.LoginAppMgr
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
