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
using Lazynet.Core.Cache;
using Lazynet.Core.Logger;
using Lazynet.Core.Service;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lazynet.AppCore
{
    /// <summary>
    /// app manger
    /// </summary>
    public class LazynetAppManager
    {
        private static LazynetAppManager appManager = null;
        public LazynetAppContext Context { get; set; }
        public ILazynetStartup Startup { get; set; }

        static LazynetAppManager()
        {
            appManager = new LazynetAppManager();
        }

        public static LazynetAppManager GetInstance()
        {
            return appManager;
        }

        public LazynetAppManager UseStartup<T>() where T: ILazynetStartup
        {
            this.Startup = typeof(T).CreateInstance<ILazynetStartup>();
            return this;
        }

        public LazynetAppManager Log(string content)
        {
            this.Context.Logger.Info(content);
            return this;
        }

        public LazynetAppManager Builder()
        {
            try
            {
                // 配置参数
                this.Context = new LazynetAppContext();
                this.Context.Config = new LazynetAppConfig();
                this.Startup?.Configuration(this.Context.Config);

                // 初始化
                this.Context.Logger = new LazynetLogger();
                this.Context.Timer = new LazynetAppTimer();
                this.Context.Cache = new LazynetRedis(this.Context.Config.RedisHost, this.Context.Config.RedisPassword);

                // 配置过滤器
                this.Context.AppFilter = new LazynetAppFilter();
                this.Startup?.ConfigureFilter(this.Context.AppFilter);

                // 配置服务
                this.Context.Service = new LazynetAppService(this.Context);
                this.Startup?.ConfigureServices(this.Context.Service);

                // 初始化服务器
                this.Context.Server = new LazynetAppServer(this.Context);
            }
            catch(Exception ex)
            {
                this.Context.AppFilter.ExpcetionFilter?.OnException(ex);
            }

            return this;
        }

        public void Start()
        {
            try
            {
                this.Context.Logger.Info("login app start");
                this.Context.Server.Connect();
            }
            catch (Exception ex)
            {
                this.Context.AppFilter.ExpcetionFilter?.OnException(ex);
            }
        }

    }
}
