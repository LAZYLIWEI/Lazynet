/*
* ==============================================================================
*
* Filename: SocketEvent
* Description: 
*
* Version: 1.0
* Created: 2020/5/4 1:06:20
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.LUA;
using Lazynet.Core.Network;
using Lazynet.Core.Network.Server;
using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.GameServer
{
    public class SocketEvent : ILazynetSocketEvent
    {
        public LuaTable Table { get; }
        public SocketEvent(LuaTable table)
        {
            this.Table = table;
        }

        public void Connect(LazynetHandlerContext ctx)
        {
            this.Table.CallFunction("onConnect", ctx);
        }

        public void DisConnect(LazynetHandlerContext ctx)
        {
            this.Table.CallFunction("onDisConnect", ctx);
        }

        public void Exception(LazynetHandlerContext ctx, Exception exception)
        {
            this.Table.CallFunction("onException", ctx, exception.ToString());
        }

        public void Read(LazynetHandlerContext ctx, string msg)
        {
            this.Table.CallFunction("onRead", ctx, msg);
        }

    }

}
