using System;

namespace Lazynet.LoginApp
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
