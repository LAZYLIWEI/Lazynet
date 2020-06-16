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

namespace Lazynet.AppMgrCore
{
    public class LazynetExternalServerHandler
    {
        public LazynetExternalServerContext ServerContext { get; }
        public LazynetAppContext Context { get; }

        public LazynetExternalServerHandler(LazynetExternalServerContext serverContext, LazynetAppContext context)
        {
            this.ServerContext = serverContext;
            this.Context = context;
        }

        public void Visit(LazynetHandlerContext ctx)
        {
            string ID = EncryptionHelper.GetMD5Hash(ctx.GetAddress());
            this.Context.Logger.Info("Node's ID is " + ID + " visit ");
        }

        public void Connect(LazynetHandlerContext ctx)
        {
            string ID = EncryptionHelper.GetMD5Hash(ctx.GetAddress());
            this.ServerContext.ChildNodeCollection.Add(ID, new LazynetChildNode()
            {
                Address = ctx.GetAddress(),
                ConnectDateTime = DateTime.Now,
                Context = ctx
            });
            this.Context.Logger.Info("Node's ID is " + ID + " connect ");
        }

        public void DisConnect(LazynetHandlerContext ctx)
        {
            string ID = EncryptionHelper.GetMD5Hash(ctx.GetAddress());
            this.ServerContext.ChildNodeCollection.Remove(ID);
            this.Context.Logger.Error("Node's ID is " + ID + " disconnect ");
        }

        public void Exception(LazynetHandlerContext ctx, Exception exception)
        {
            string ID = EncryptionHelper.GetMD5Hash(ctx.GetAddress());
            this.Context.Logger.Error("Node's ID is " + ID + " exception exception message: " + exception.ToString());
        }

        public void Heartbeat(LazynetHandlerContext ctx)
        {
            // this.Context.Logger.Info(ctx.GetAddress() + " heartbeart");
        }

        public void Dispatch(LazynetHandlerContext ctx, string ID, string message)
        {
            this.Context.InteriorServer.ServerContext.Response(ID, message);
        }

    }
}
