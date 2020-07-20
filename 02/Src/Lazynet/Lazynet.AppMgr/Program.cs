using System;

namespace Lazynet.AppMgr
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
