using Lazynet.Core.Logger;
using System;

namespace Lazynet.LogApp
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
