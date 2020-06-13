/*
* ==============================================================================
*
* Filename: LazynetServiceLoader
* Description: 
*
* Version: 1.0
* Created: 2020/5/29 22:01:40
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lazynet.Core.Service
{
    /// <summary>
    /// service loader
    /// </summary>
    public class LazynetServiceLoader
    {
        public static string RouteUrlTemplete
        {
            get
            {
                return "/{0}/{1}";
            }
        }

        public static string ServiceSuffix
        {
            get
            {
                return "Service";
            }
        }


        public static List<LazynetServiceMeta> Load(Assembly ass)
        {
            // 过滤类
            List<Type> serviceTypeList = new List<Type>();
            var typeArray = ass.GetTypes();
            foreach (var item in typeArray)
            {
                if (item.IsPublic 
                    && item.IsSubclass(typeof(ILazynetApiService)))
                {
                    serviceTypeList.Add(item);
                }
            }

            // 过滤方法
            var serviceMetaList = new List<LazynetServiceMeta>();
            foreach (var classType in serviceTypeList)
            {
                serviceMetaList.AddRange(Load(classType));
            }
            return serviceMetaList;
        }


        public static List<LazynetServiceMeta> Load(Type classType)
        {
            var serviceMetaList = new List<LazynetServiceMeta>();
            var methodTypeArray = classType.GetMethods();
            foreach (var methodType in methodTypeArray)
            {
                var methodAttr = methodType.GetCustomAttribute<LazynetServiceActionAttribute>();
                if (methodType.IsPublic
                    && methodAttr != null)
                {
                    string serviceName = classType.Name.Replace(ServiceSuffix, string.Empty);
                    string routeUrl = string.Format(RouteUrlTemplete, serviceName, methodType.Name);
                    serviceMetaList.Add(new LazynetServiceMeta()
                    {
                        ClassType = classType,
                        MethodType = methodType,
                        RouteUrl = routeUrl
                    });
                }
            }

            return serviceMetaList;
        }

    }
}
