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

namespace Lazynet.AppMgrCore
{
    public class LazynetInteriorServerSocketEvent : ILazynetSocketEvent
    {
        public LazynetAppContext Context { get; }
        public LazynetInteriorServerContext ServerContext { get; }

        public LazynetInteriorServerSocketEvent(LazynetAppContext context, LazynetInteriorServerContext serverContext)
        {
            this.Context = context;
            this.ServerContext = serverContext;
        }

        public void Connect(LazynetHandlerContext ctx)
        {
            string ip = ctx.GetAddress();
            string id = EncryptionHelper.GetMD5Hash(ip);
            this.ServerContext.SessionDict.Add(id, new LazynetSession()
            {
                Address = ip,
                ConnectDateTime = DateTime.Now,
                Context = ctx
            });

            this.Dispatch(ctx, new LazynetFromMessage()
            {
                Parameters = null,
                RouteUrl = LazynetActionConstant.SessionConnect
            });

            Context.Logger.Info(ip + "connect");
        }

        public void DisConnect(LazynetHandlerContext ctx)
        {
            string ip = ctx.GetAddress();
            string id = EncryptionHelper.GetMD5Hash(ip);
            this.ServerContext.SessionDict.Remove(id);

            this.Dispatch(ctx, new LazynetFromMessage() { 
                 Parameters = null,
                 RouteUrl = LazynetActionConstant.SessionDisconnect
            });

            Context.Logger.Info(ip + "disconnect");
        }

        public void Exception(LazynetHandlerContext ctx, Exception exception)
        {
            Context.Logger.Info(ctx.GetAddress() + "exception");
        }

        public void Read(LazynetHandlerContext ctx, string msg)
        {
            // 转发消息
            var message = SerializeHelper.Deserialize<LazynetFromMessage>(msg);
            this.Dispatch(ctx, message);
        }

        /// <summary>
        /// 转发消息
        /// </summary>
        /// <param name="ctx"></param>
        private void Dispatch(LazynetHandlerContext ctx, LazynetFromMessage message)
        {
            string id = EncryptionHelper.GetMD5Hash(ctx.GetAddress());
            if (!this.ServerContext.SessionDict.ContainsKey(id))
            {
                Context.Logger.Warn("don't have the id = " + id);
                return;
            }
            message.SessionID = id;
            this.Context.ExternalServer.ServerContext.Dispatch(message);
        }

    }
}
