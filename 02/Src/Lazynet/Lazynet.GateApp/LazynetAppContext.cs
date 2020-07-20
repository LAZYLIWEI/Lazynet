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
using Lazynet.AppCore;
using Lazynet.Core.Logger;
using Lazynet.Core.LUA;
using Lazynet.Core.Network;
using Lazynet.Core.Proto;
using Lazynet.Core.Timer;
using Lazynet.Core.Util;
using Neo.IronLua;
using System.Diagnostics;

namespace Lazynet.GateApp
{
    public class LazynetAppContext : ILazynetAppContext
    {
        public string Name { get; set; }
        public LazynetAppConfig Config { get; set; }
        public ILazynetLogger Logger { get; set; }
        public LazynetTimerManager Timer { get; set; }
        public LazynetLua Lua { get; set; }
        public LazynetExternalServer ExternalServer { get; set; }
        public LazynetInteriorServer InteriorServer { get; set; }
        public LazynetAppService Service { get; set; }
        public LazynetSessionManager SessionManager { get; set; }

        public LazynetAppContext()
        {
            this.Name = "GateApp";
        }

        /// <summary>
        /// 配置外部服务器
        /// </summary>
        /// <param name="port"></param>
        /// <param name="heartbeat"></param>
        /// <param name="type"></param>
        public void SetExternalServerConfig(int port, int heartbeat, LazynetSocketType type, string websocketPath)
        {
            this.Config.ExternalServerHeartbeat = heartbeat;
            this.Config.ExternalServerPort = port;
            this.Config.ExternalServerType = type;
            this.Config.ExternalServerWebsocketPath = websocketPath;
        }

        /// <summary>
        /// 配置内部服务器
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="type"></param>
        public void SetInteriorServerConfig(string ip, int port, LazynetSocketType type)
        {
            this.Config.InteriorServerIP = ip;
            this.Config.InteriorServerPort = port;
            this.Config.InteriorServerType = type;
        }

        public void AddService(LuaTable table)
        {
            this.Service.AddService(table);
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

        public void AddSession(LazynetHandlerContext ctx)
        {
            this.SessionManager.AddSession(ctx);
        }

        public LazynetSession GetSessionByCtx(LazynetHandlerContext ctx)
        {
            string address = ctx.GetAddress();
            string id = EncryptionHelper.GetMD5Hash(address);
            return this.SessionManager.GetSession(id);
        }

        public LazynetSession GetSessionByID(string id)
        {
            return this.SessionManager.GetSession(id);
        }

        public void RemoveSession(LazynetHandlerContext ctx)
        {
            string address = ctx.GetAddress();
            string id = EncryptionHelper.GetMD5Hash(address);
            this.SessionManager.RemoveSession(id);
        }

        public void SendMessage(LazynetHandlerContext ctx, string routeUrl, LuaTable args)
        {
            if (ctx == null)
            {
                var method = new StackFrame(1).GetMethod();
                this.Log(LazynetLogLevel.Warn, method.Name + ": ctx is null ");
                return;
            }
            LazynetMessage message = new LazynetMessage();
            message.RouteUrl = routeUrl;
            foreach (var item in args)
            {
                message.Parameters.Add(item.Value);
            }
            string msg = SerializeHelper.Serialize(message);
            this.Log(LazynetLogLevel.Debug, "===send data is " + msg);
            ctx.WriteAndFlushAsync(msg);
        }

        internal void CallService(LazynetMessage message)
        {
            if (message != null)
            {
                this.Service.CallService(message);
            }
            else
            {
                this.Logger.Warn(string.Format("message is null"));
            }
        }


    }

}
