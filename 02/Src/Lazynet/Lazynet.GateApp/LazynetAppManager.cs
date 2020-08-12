/*
* ==============================================================================
*
* Filename: LazynetAppManager
* Description: 
*
* Version: 1.0
* Created: 2020/6/13 10:06:02
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
using Lazynet.Core.Timer;
using Lazynet.Core.Util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lazynet.GateApp
{
    public class LazynetAppManager
    {
        private static LazynetAppManager appManager = null;
        public LazynetAppContext Context { get; set; }

        static LazynetAppManager()
        {
            appManager = new LazynetAppManager();
        }

        public static LazynetAppManager GetInstance()
        {
            return appManager;
        }

        public LazynetAppManager Builder()
        {
            try
            {
                // 获取配置文件配置
                var configInfo = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true)
                    .Build();
                var luaScriptConfiguration = configInfo.GetSection("Script");

                // 初始化并配置参数
                this.Context = new LazynetAppContext();
                this.Context.Config = new LazynetAppConfig();

                this.Context.Logger = new LazynetLogger("log4net.config");
                this.Context.Timer = new LazynetTimerManager();

                // session manager
                this.Context.SessionManager = new LazynetSessionManager();

                // service
                this.Context.ActionProxy = new LazynetActionProxy(this.Context);

                // lua
                this.Context.Lua = new LazynetLua();
                this.Context.Lua.DoFile(luaScriptConfiguration["MainFile"], luaScriptConfiguration["RootDir"]);
            }
            catch (Exception ex)
            {
                this.Context.Log(LazynetLogLevel.Error, ex.ToString());
            }

            return this;
        }

        public void Start()
        {
            try
            {
                // 启动服务器
                this.Context.ExternalServer = new LazynetExternalServer(this.Context);
                this.Context.InteriorServer = new LazynetInteriorServer(this.Context);
                this.Context.ExternalServer.Start();
                this.Context.InteriorServer.Start();
            }
            catch (Exception ex)
            {
                this.Context.Log(LazynetLogLevel.Error, ex.ToString());
            }
        }


    }
}
