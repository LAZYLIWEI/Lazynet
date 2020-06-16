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
using Lazynet.Core.Logger;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.AppMgrCore
{
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


        public LazynetAppManager UseStartup<T>() where T : ILazynetStartup
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
                // 初始化并配置参数
                this.Context = new LazynetAppContext();
                this.Context.Config = new LazynetAppConfig();
                this.Startup?.Configuration(this.Context.Config);

                this.Context.Logger = new LazynetLogger();
                this.Context.Timer = new LazynetAppTimer();

                // 配置过滤器
                this.Context.AppFilter = new LazynetAppFilter();
                this.Startup?.ConfigureFilter(this.Context.AppFilter);

                this.Context.ExternalServer = new LazynetExternalServer(this.Context);
                this.Context.InteriorServer = new LazynetInteriorServer(this.Context);
            }
            catch (Exception ex)
            {
                this.Context.AppFilter.ExpcetionFilter?.OnException(ex);
            }

            return this;
        }

        public void Start()
        {
            try
            {
                this.Context.ExternalServer.Start();
                this.Context.InteriorServer.Start();
            }
            catch (Exception ex)
            {
                this.Context.AppFilter.ExpcetionFilter?.OnException(ex);
            }
        }


    }
}
