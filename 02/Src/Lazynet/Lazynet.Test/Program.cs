using Lazynet.Core.Logger;
using Lazynet.Core.LUA;
using System;

namespace Lazynet.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ILazynetLua lua = new LazynetLua();
            lua.DoFile("main.lua", "./Script");

            

            Console.ReadKey();
        }
    }

}
