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
               .Builder();
            Console.ReadKey();
        }
    }
}
