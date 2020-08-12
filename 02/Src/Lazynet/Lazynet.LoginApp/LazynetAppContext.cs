/*
* ==============================================================================
*
* Filename: AppContext
* Description: 
*
* Version: 1.0
* Created: 2020/5/30 1:30:55
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
using Lazynet.DB;
using Neo.IronLua;
using System;

namespace Lazynet.LoginApp
{
    public class LazynetAppContext : ILazynetContext
    {
        public string Name { get; set; }
        public LazynetAppConfig Config { get; set; }
        public LazynetActionProxy ActionProxy { get; set; }
        public LazynetLua Lua { get; set; }
        public LazynetAppServer Server { get; set; }
        public ILazynetLogger Logger { get; set; }
        public LazynetTimerManager Timer { get; set; }
        public DBProxy DBProxy { get; set; }

        public LazynetAppContext()
        {
            this.Name = "LoginApp";
        }

        public void SendMessage(LazynetHandlerContext ctx, string routeUrl, LuaTable args)
        {
            LazynetMessage message = new LazynetMessage();
            message.RouteUrl = routeUrl;
            foreach (var item in args)
            {
                message.Parameters.Add(item.Value);
            }
            ctx.WriteAndFlushAsync(SerializeHelper.Serialize(message));
        }

        public void AddAction(LuaTable table)
        {
            this.ActionProxy.Add(table);
        }

        public string AddJob(int repeatCount, int interval, Func<LuaTable, int> callFunction, LuaTable parameters)
        {
            string name = this.Timer.AddJob(repeatCount, interval, callFunction, parameters);
            return name;
        }

        public void RemoveJob(string name)
        {
            this.Timer.RemoveJob(name);
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
