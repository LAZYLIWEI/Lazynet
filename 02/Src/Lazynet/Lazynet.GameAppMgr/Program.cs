using Lazynet.AppMgrCore;
using System;

namespace Lazynet.GameAppMgr
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
