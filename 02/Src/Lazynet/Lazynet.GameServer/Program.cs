using Lazynet.Core.Logger;
using Lazynet.Core.LUA;
using Lazynet.Core.Network;
using Lazynet.Core.Network.Client;
using Lazynet.Core.Timer;
using Lazynet.LuaCore;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Lazynet.GameServer
{

    class Program
    {
        static void Main(string[] args)
        {

            ILazynetClient client = new LazynetClient(new LazynetClientConfig() {
                IP = "127.0.0.1",
                Port = 20000,
                SocketType = LazynetSocketType.TcpSocket
            });
            ILazynetLogger logger = new LazynetLogger();
            bool connectResult = client.ConnectToHost();
            if (connectResult)
            {
                logger.Log("连接成功");
            }

            //ILazynetLua lua = new LazynetLua();
            //LazynetOpenApiLoadder.Load(lua);
            //lua.RegisterPackage("gameServer", typeof(LuaOpenApi));
            //lua.DoFile("main.lua");

            Console.ReadKey();
        }
    }
}
