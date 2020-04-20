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

            //var testService = client.CreateSharpService();
            //GateHandler handler = new GateHandler(testService);
            //testService.AddTrigger("/GateHandler/PrintHelloWorld", new LazynetSharpTrigger<GateHandler>(handler, handler.GetType().GetMethod("PrintHelloWorld")));
            //testService.SetAlias("test");
            //testService.Start();

            //var server = testService.CreateSocket();
            //testService.BindAsync(server);

            Console.ReadKey();
        }
    }
}
