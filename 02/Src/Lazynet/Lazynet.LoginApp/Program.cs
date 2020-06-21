using Lazynet.AppCore;
using Lazynet.Core.Timer;
using Lazynet.Core.Util;
using Lazynet.LoginApp.Job;
using Neo.IronLua;
using System;
using System.Collections.Generic;

namespace Lazynet.LoginApp
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
