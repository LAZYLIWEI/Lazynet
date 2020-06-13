using Lazynet.Core;
using System;

namespace Lazynet.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            LazynetClient client = new LazynetClient(new LazynetConfig() { });
            client.DispatchMessage();

            // 创建bootstrap服务
            var bootstrapService = client.CreateLuaService("./lua/bootstrap.lua");
            bootstrapService.Start();

            Console.ReadKey();
        }
    }
}
