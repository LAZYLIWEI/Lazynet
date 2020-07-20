/*
* ==============================================================================
*
* Filename: AppLoader
* Description: 
*
* Version: 1.0
* Created: 2020/5/29 22:39:24
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
using Lazynet.Core.Timer;
using Lazynet.DB;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Lazynet.LoginApp
{
    /// <summary>
    /// login app
    /// </summary>
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

                // 配置参数
                this.Context = new LazynetAppContext();
                this.Context.Config = new LazynetAppConfig();

                // 初始化
                this.Context.Timer = new LazynetTimerManager();

                // log
                this.Context.Logger = new LazynetLogger();

                // db
                this.Context.DBProxy = new DBProxy();

                // 服务
                this.Context.Service = new LazynetAppService(this.Context);

                // lua
                this.Context.Lua = new LazynetLua();
                this.Context.Lua.DoFile(luaScriptConfiguration["MainFile"], luaScriptConfiguration["RootDir"]);
            }
            catch(Exception ex)
            {
                this.Context.Log(LazynetLogLevel.Error,  ex.ToString());
            }

            return this;
        }

        public void Start()
        {
            try
            {
                // 初始化服务器
                this.Context.Server = new LazynetAppServer(this.Context);
                this.Context.Server.Connect();
            }
            catch (Exception ex)
            {
                this.Context.Log(LazynetLogLevel.Error,  ex.ToString());
            }
        }

        

    }
}
