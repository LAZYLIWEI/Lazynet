using Lazynet.AppCore;
using System;

namespace Lazynet.GameApp
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
