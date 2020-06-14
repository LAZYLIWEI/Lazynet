/*
* ==============================================================================
*
* Filename: AppService
* Description: 
*
* Version: 1.0
* Created: 2020/5/30 12:33:46
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Network;
using Lazynet.Core.Proto;
using Lazynet.Core.Service;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginApp.AppStart
{
    public class LazynetAppService
    {
        public Dictionary<string, ILazynetService> ServiceDict { get; }
        public LazynetAppContext Context { get; }

        public LazynetAppService(LazynetAppContext context)
        {
            this.ServiceDict = new Dictionary<string, ILazynetService>();
            this.Context = context;
        }

        public ILazynetService Get(string name)
        {
            if (!this.Contains(name))
            {
                return default(ILazynetService);
            }
            return this.ServiceDict[name];
        }

        public bool Contains(string name)
        {
            return this.ServiceDict.ContainsKey(name);
        }

        public void AddService(LazynetServiceMeta serviceMeta, LazynetServiceType type)
        {
            var instance = serviceMeta.ClassType.CreateInstance(this.Context);
            this.ServiceDict.Add(serviceMeta.RouteUrl, new LazynetSharpService(type, instance, serviceMeta.MethodType));
        }

        public void AddServices<T>(LazynetServiceType type) where T : LazynetBaseService
        {
            var serviceMetaList = LazynetServiceLoader.Load(typeof(T));
            foreach (var item in serviceMetaList)
            {
                var attr = item.ClassType.GetCustomAttribute<LazynetServiceTypeAttribute>();
                if (attr == null)
                {
                    AddService(item, LazynetServiceType.Normal);
                }
                else
                {
                    AddService(item, attr.Type);
                }
            }
        }

        public void AddSystemServices<T>() where T : LazynetBaseService
        {
            AddServices<T>(LazynetServiceType.System);
        }

        public void AddNormalServices<T>() where T : LazynetBaseService
        {
            AddServices<T>(LazynetServiceType.Normal);
        }


        public object[] CallService(LazynetMessage message)
        {
            if (message == null)
            {
                this.Context.Logger.Warn("message deserialize failed, msg is null");
                return null;
            }
            var service = this.Get(message.RouteUrl);
            if (service == null)
            {
                this.Context.Logger.Warn(string.Format("don't have the routeurl, routeUrl is {0}", message.RouteUrl));
                return null;
            }

            var result = service.CallBack(new LazynetServiceEntity()
            {
                Type = service.Type,
                RouteUrl = message.RouteUrl,
                Parameters = message.Parameters?.ToArray()
            });
            return result;
        }
    }
}
