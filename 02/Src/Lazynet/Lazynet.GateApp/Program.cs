using System;

namespace Lazynet.GateApp
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
