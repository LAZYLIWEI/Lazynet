/*
* ==============================================================================
*
* Filename: LazynetAppContext
* Description: 
*
* Version: 1.0
* Created: 2020/6/13 10:06:45
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Action;
using Lazynet.Core.Logger;
using Lazynet.Core.LUA;
using Lazynet.Core.Network;
using Lazynet.Core.Proto;
using Lazynet.Core.Timer;
using Lazynet.Core.Util;
using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.AppMgr
{
    public class LazynetAppContext : ILazynetContext
    {
        public string Name { get; set; }
        public LazynetAppConfig Config { get; set; }
        public LazynetTimerManager Timer { get; set; }
        public LazynetLua Lua { get; set; }
        public LazynetActionProxy ActionProxy { get; set; }
        public LazynetAppServer Server { get; set; }
        public LazynetLogger Logger { get; set; }
        public LazynetNodeCollection Nodes { get; set; }

        public LazynetAppContext()
        {
            this.Name = "AppMgr";
        }

        internal void Call(LazynetMessage message)
        {
            try
            {
                var result = this.ActionProxy.Call(message);
                if (result == null)
                {
                    this.Log(LazynetLogLevel.Warn, "call ActionProxy fail, route is " + message.RouteUrl);
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.ToString());
            }
        }

        public void SetConfigInfo(int port, int heartbeat, LazynetSocketType socketType)
        {
            this.Config.Port = port;
            this.Config.Heartbeat = heartbeat;
            this.Config.SocketType = socketType;
        }

        public void AddActionProxy(LuaTable table)
        {
            this.ActionProxy.Add(table);
        }

        public string AddJob(int repeatCount, int interval, Func<LuaTable, int> callFunction, LuaTable parameters)
        {
            string name = this.Timer.AddJob(repeatCount, interval, callFunction, parameters);
            return name;
        }


        public LazynetNode GetNodeByName(string name)
        {
            var node = this.Nodes.GetByName(name);
            return node;
        }

        public void RemoveJob(string name)
        {
            this.Timer.RemoveJob(name);
        }

        public void AddNode(string name, LazynetHandlerContext ctx)
        {
            if (string.IsNullOrEmpty(name)
                || ctx == null)
            { 
                this.Logger.Error(" name is empty or ctx is null, so add node fail ");
                return;
            }
            this.Nodes.Add(name, ctx);
        }

        public void SendMessage(LazynetNode node, string msg)
        {
            if (node == null)
            {
                this.Log(LazynetLogLevel.Warn, node.Name + "'s node is null");
                return;
            }

            this.Log(LazynetLogLevel.Debug, "===send data is " + msg);
            node.Context.WriteAndFlushAsync(msg);
        }

        public void RemoveNode(LazynetHandlerContext ctx)
        {
            string id = EncryptionHelper.GetMD5Hash(ctx.GetAddress());
            this.Nodes.RemoveByID(id);
        }

        public void Log(LazynetLogLevel level, string content)
        {
            switch (level)
            {
                case LazynetLogLevel.Error:
                    this.Logger.Error(content);
                    break;
                case LazynetLogLevel.Warn:
                    this.Logger.Warn(content);
                    break;
                case LazynetLogLevel.Debug:
                    this.Logger.Debug(content);
                    break;
                case LazynetLogLevel.Info:
                    this.Logger.Info(content);
                    break;
            }
        }

    }
}
