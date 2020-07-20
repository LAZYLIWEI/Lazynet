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
using Lazynet.AppCore;
using Lazynet.Core.Logger;
using Lazynet.Core.LUA;
using Lazynet.Core.Network;
using Lazynet.Core.Proto;
using Lazynet.Core.Timer;
using Lazynet.Core.Util;
using Neo.IronLua;
using System;

namespace Lazynet.LogApp
{
    public class LazynetAppContext : ILazynetAppContext
    {
        public string Name { get; set; }
        public LazynetAppConfig Config { get; set; }
        public LazynetAppService Service { get; set; }
        public LazynetLua Lua { get; set; }
        public LazynetAppServer Server { get; set; }
        public ILazynetLogger Logger { get; set; }
        public LazynetTimerManager Timer { get; set; }
        public LazynetHandlerContext CurrentContext { get; set; }

        public LazynetAppContext()
        {
            this.Name = "LogApp";
        }

        public void SendMessage(string routeUrl, LuaTable args)
        {
            LazynetMessage message = new LazynetMessage();
            message.RouteUrl = routeUrl;
            foreach (var item in args)
            {
                message.Parameters.Add(item.Value);
            }
            this.CurrentContext?.WriteAndFlushAsync(SerializeHelper.Serialize(message));
        }

        public void AddService(LuaTable table)
        {
            this.Service.AddService(table);
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

        public void Log(LazynetLogLevel level, string name, string content)
        {
            string tmp = string.Format("{0} {1}", name, content);
            this.Log(level, tmp);
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
