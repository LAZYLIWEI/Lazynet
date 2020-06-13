/*
* ==============================================================================
*
* Filename: LazynetRoute
* Description: 
*
* Version: 1.0
* Created: 2020/3/22 23:59:13
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lazynet.Core
{
    public class LazynetRoute
    {
        //public string Suffix { get { return "Handler"; } }
        //public string RouteTemplete { get { return "/{0}/{1}"; } }
        //public Dictionary<string, LazynetSharpTrigger> RouteEntities { get; }


        //public LazynetRoute()
        //{
        //    this.RouteEntities = new Dictionary<string, LazynetSharpTrigger>();
        //}

        //public void InitMappingInfo(Assembly ass)
        //{
        //    var types = ass.GetTypes();
        //    foreach (var type in types)
        //    {
        //        if (type.IsPublic && type.Name.Contains(this.Suffix))
        //        {
        //            foreach (var method in type.GetMethods())
        //            {
        //                if (method.IsPublic)
        //                {
        //                    string routeUrl = string.Format(RouteTemplete, type.Name, method.Name);
        //                    this.RouteEntities.Add(routeUrl, new LazynetSharpTrigger()
        //                    {
        //                        MethodInfo = method,
        //                        RouteUrl = routeUrl,
        //                        Type = type
        //                    });
        //                }
        //            }
        //        }
        //    }
        //}


        //public LazynetSharpTrigger Mapping(string routeUrl)
        //{
        //    if (this.RouteEntities is null)
        //    {
        //        throw new Exception("请在RunAsync方法之前使用[UseDefaultRouteHandler]方法");
        //    }

        //    LazynetSharpTrigger routeEntity = null;
        //    if (this.RouteEntities.ContainsKey(routeUrl))
        //    {
        //        routeEntity = this.RouteEntities[routeUrl];
        //    }
        //    return routeEntity;
        //}
    }
}
