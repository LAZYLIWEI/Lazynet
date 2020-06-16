/*
* ==============================================================================
*
* Filename: Startup
* Description: 
*
* Version: 1.0
* Created: 2020/5/30 1:18:27
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.AppCore;
using Lazynet.Core.Network;
using Lazynet.Core.Network.Client;
using Lazynet.Core.Service;
using Lazynet.DB.DAL;
using Lazynet.LoginApp.Filter;
using System.Reflection;

namespace Lazynet.LoginApp
{
    public class Startup : ILazynetStartup
    {
        public void Configuration(LazynetAppConfig config)
        {
            config.RedisHost = "47.92.213.250";
            config.RedisPassword = "8g199696QQ";
            config.NetworkConfig = new LazynetClientConfig() {
                IP = "127.0.0.1",
                Port = 20000,
                SocketType = LazynetSocketType.TcpSocket
            };
            DBContextFactory.ConnectionString = "Data Source=47.92.213.250;Initial Catalog=LazyGame;Persist Security Info=True;User ID=sa;Password=8g199696QQ";
        }

        public void ConfigureServices(LazynetAppService appService)
        {
            // load service
            var serviceMetaList = LazynetServiceLoader.Load(Assembly.GetExecutingAssembly());
            foreach (var item in serviceMetaList)
            {
                var attr = item.ClassType.GetCustomAttribute<LazynetServiceTypeAttribute>();
                if (attr == null)
                {
                    appService.AddService(item, LazynetServiceType.Normal);
                }
                else
                {
                    appService.AddService(item, attr.Type);
                }
            }
        }

        public void ConfigureFilter(LazynetAppFilter filters)
        {
            filters.ExpcetionFilter = new MyExptionFilter();
            filters.ActionFilter = new MyActionFilter();
        }
    }
}
