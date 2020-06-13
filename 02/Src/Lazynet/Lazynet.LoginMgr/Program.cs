using Lazynet.Core.Cache;
using Lazynet.Core.Logger;
using Lazynet.Core.Network.Server;
using System;

namespace Lazynet.LoginMgr
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerMgr.GetInstance().Log("login app mgr start");

            // 内部服务器
            ExternalServer externalServer = new ExternalServer();

            // 外部服务器
            InteriorServer interiorServer = new InteriorServer();

            // 设置参数并启动
            externalServer.NoticeDispatchEventNotify(interiorServer.Response);
            interiorServer.NoticeReadEventNotify(externalServer.Dispatch);
            interiorServer.Start();
            externalServer.Start();

            Console.ReadKey();
        }
    }
}
