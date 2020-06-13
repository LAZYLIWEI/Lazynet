/*
* ==============================================================================
*
* Filename: InteriorServerSocketEvent
* Description: 
*
* Version: 1.0
* Created: 2020/5/21 23:45:55
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Network;
using Lazynet.Core.Proto;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginMgr
{
    public class InteriorServerSocketEvent : ILazynetSocketEvent
    {
        public event Action<LazynetMessage> ReadEventNotify;
        public Dictionary<string, Session> SessionDict { get; }
        public InteriorServerSocketEvent(Dictionary<string, Session> sessionDict, Action<LazynetMessage> readEventNotify)
        {
            this.ReadEventNotify = readEventNotify;
            this.SessionDict = sessionDict;
        }

        public void Connect(LazynetHandlerContext ctx)
        {
            string ip = ctx.GetAddress();
            string id = EncryptionHelper.GetMD5Hash(ip);
            this.SessionDict.Add(id, new Session()
            {
                Address = ip,
                ConnectDateTime = DateTime.Now,
                Context = ctx
            });
            LoggerMgr.GetInstance().Log("connect " + ip);
        }

        public void DisConnect(LazynetHandlerContext ctx)
        {
            string ip = ctx.GetAddress();
            string id = EncryptionHelper.GetMD5Hash(ip);
            this.SessionDict.Remove(id);
            LoggerMgr.GetInstance().Log("disConnect " + ip);
        }

        public void Exception(LazynetHandlerContext ctx, Exception exception)
        {
            LoggerMgr.GetInstance().Log("exception " + ctx.GetAddress());
        }

        public void Read(LazynetHandlerContext ctx, string msg)
        {
            // 校验
            string id = EncryptionHelper.GetMD5Hash(ctx.GetAddress());
            if (!this.SessionDict.ContainsKey(id))
            {
                LoggerMgr.GetInstance().Log("don't have the id " + id);
                return;
            }

            // 转发消息
            var message = SerializeHelper.Deserialize<LazynetFromMessage>(msg);
            message.SessionID = id;
            this.ReadEventNotify(message);
        }

    }
}
