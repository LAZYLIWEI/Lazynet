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

namespace Lazynet.Gate
{
    class Program
    {
        static void Main(string[] args)
        {
            LazynetClient client = new LazynetClient(new LazynetConfig() { 

            });
            client.UseRoute(Assembly.GetExecutingAssembly()).DispatchMessage();
            LazynetService gateService = client.CreateService();
            gateService.Start("gate");
            LazynetService dataCenterService = client.CreateService();
            dataCenterService.Start("dataCenter");

            GateHandler gate = new GateHandler();
            dataCenterService.SendMessage(gateService.GetID(), new LazynetServiceMessage(gate, "/GateHandler/KillService", null));



            LazynetLua lua = new LazynetLua();
            lua.DoChunk("./lua/gate.lua");


            Console.ReadKey();
        }
    }
}
