/*
* ==============================================================================
*
* Filename: LazynetRouteEntity
* Description: 
*
* Version: 1.0
* Created: 2020/3/22 23:59:50
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
    public class LazynetRouteEntity
    {
        public string RouteUrl { get; set; }
        public Type Type { get; set; }
        public MethodInfo MethodInfo { get; set; }

        public object CallMethod(LazynetServiceContext context, LazynetServiceMessage message)
        {
            List<object> parameters = new List<object>();
            parameters.Add(context);
            if (message.Parameters != null)
            {
                parameters.AddRange(parameters);
            }
            var result = this.MethodInfo?.CallMethod(message.Instance, parameters.ToArray());
            return result;
        }

    }
}
