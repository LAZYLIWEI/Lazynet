/*
* ==============================================================================
*
* Filename: InteriorServer
* Description: 
*
* Version: 1.0
* Created: 2020/5/22 23:21:13
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Network;
using Lazynet.Core.Network.Server;
using Lazynet.Core.Proto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginMgr
{
    /// <summary>
    /// 外部服务器
    /// </summary>
    public class InteriorServer
    {
        public ILazynetServer Server { get; }
        public Dictionary<string, Session> SessionDict { get; }

        public event Action<LazynetMessage> ReadEventNotify; 

        public InteriorServer()
        {
            this.Server = new LazynetServer(new LazynetServerConfig()
            {
                Heartbeat = 3000,
                Port = 10000,
                SocketType = LazynetSocketType.Websocket,
                WebsocketPath = "/ws",
            });
            this.SessionDict = new Dictionary<string, Session>();
        }

        public void NoticeReadEventNotify(Action<LazynetMessage> action)
        {
            this.ReadEventNotify += action;
        }


        public void Response(string ID, string message)
        {
            // 校验
            if (!this.SessionDict.ContainsKey(ID))
            {
                LoggerMgr.GetInstance().Log(" dont's have the ID: " + ID);
                return;
            }
            var session = this.SessionDict[ID];
            session.Context.WriteAndFlushAsync(message);
        }

        public void Start()
        {
            this.Server.SetSocketEvent(new InteriorServerSocketEvent(this.SessionDict, this.ReadEventNotify));
            this.Server.Bind();
            LoggerMgr.GetInstance().Log("interior server bind port:" + Server.GetPort());
        }

    }
}
