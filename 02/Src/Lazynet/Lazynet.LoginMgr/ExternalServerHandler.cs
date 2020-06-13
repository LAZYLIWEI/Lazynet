/*
* ==============================================================================
*
* Filename: ExternalServerHandler
* Description: 
*
* Version: 1.0
* Created: 2020/5/21 23:58:11
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Network;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginMgr
{
    public class ExternalServerHandler
    {
        public ChildNodeCollection ChildNodeCollection { get; }
        public event Action<string, string> DispatchEventNotify;

        public ExternalServerHandler(ChildNodeCollection childNodeCollection, Action<string, string> dispatchEventNotify)
        {
            this.ChildNodeCollection = childNodeCollection;
            this.DispatchEventNotify = dispatchEventNotify;
        }

        public void Visit(LazynetHandlerContext ctx)
        {
            LoggerMgr.GetInstance().Log(ctx.GetAddress() + " visit ");
        }

        public void Connect(LazynetHandlerContext ctx)
        {
            string ID = EncryptionHelper.GetMD5Hash(ctx.GetAddress());
            this.ChildNodeCollection.Add(ID, new ChildNode()
            {
                Address = ctx.GetAddress(),
                ConnectDateTime = DateTime.Now,
                Context = ctx
            });
            LoggerMgr.GetInstance().Log(ctx.GetAddress() + " connect ");
        }

        public void DisConnect(LazynetHandlerContext ctx)
        {
            string ID = EncryptionHelper.GetMD5Hash(ctx.GetAddress());
            this.ChildNodeCollection.Remove(ID);
            LoggerMgr.GetInstance().Log(ctx.GetAddress() + " disconnect ");
        }

        public void Exception(LazynetHandlerContext ctx, Exception exception)
        {
            LoggerMgr.GetInstance().Log(ctx.GetAddress() + " exception message: " + exception.ToString());
        }

        public void Heartbeat(LazynetHandlerContext ctx)
        {
            // LoggerMgr.GetInstance().Log(ctx.GetAddress() + " heartbeart");
        }

        public void Dispatch(LazynetHandlerContext ctx, string ID, string message)
        {
            this.DispatchEventNotify(ID, message);
        }

    }
}
