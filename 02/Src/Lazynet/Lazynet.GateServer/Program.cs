using Lazynet.Core.LUA;
using Lazynet.Core.Network.Client;
using Lazynet.Core.Network.Server;
using Lazynet.LuaCore;
using System;

namespace Lazynet.GateServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //ILazynetLua lua = new LazynetLua();
            //LazynetOpenApiLoadder.Load(lua);
            //lua.RegisterPackage("gateServer", typeof(LuaOpenApi));
            //lua.DoFile("main.lua");

            // 外部服务器
            ILazynetServer interiorServer = new LazynetServer(new LazynetServerConfig()
            {
                Heartbeat = 3000,
                Port = 10000,
                SocketType = Core.Network.LazynetSocketType.Websocket,
                WebsocketPath = "ws",
            });
            interiorServer.SetSocketEvent(new InteriorServerSocketEvent());
            interiorServer.Bind();

            // 内部服务器
            ILazynetServer externalServer = new LazynetServer(new LazynetServerConfig()
            {
                Heartbeat = 3000,
                Port = 20000,
                SocketType = Core.Network.LazynetSocketType.TcpSocket,
            });
            externalServer.SetSocketEvent(new InteriorServerSocketEvent());
            externalServer.Bind();
            Console.ReadKey();
        }
    }
}
