using DotNetty.Transport.Bootstrapping;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Threading.Tasks;
using DotNetty.Codecs.Http;
using Lazynet.Gate.First;
using Lazynet.Gate.Three;
using Lazynet.Core;
using System.Threading;
using System.Reflection;
using Lazynet.LUA;

namespace Lazynet.Gate
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

            //var testService = client.LazynetSharpService();
            //GateHandler handler = new GateHandler(testService);
            //testService.AddTrigger("/GateHandler/PrintHelloWorld", new LazynetSharpTrigger<GateHandler>(handler, handler.GetType().GetMethod("PrintHelloWorld")));
            //testService.SetAlias("test");
            //testService.Start();

            Console.ReadKey();
        }
    }
}
