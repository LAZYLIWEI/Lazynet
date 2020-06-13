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
using Lazynet.Util;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lazynet.Core
{
    public class LazynetSharpTrigger<T> : ILazynetTrigger where T: LazynetTriggerProvider
    {
        public MethodInfo MethodInfo { get; set; }
        public T Instance { get; set; }

        public LazynetSharpTrigger(T instance, MethodInfo methodInfo)
        {
            this.Instance = instance;
            this.MethodInfo = methodInfo;
        }

        public object[] CallBack(LazynetServiceMessage message)
        {
            var result = this.MethodInfo?.CallMethod(this.Instance, message.Parameters);
            return new object[] {
                result
            };
        }

    }
}
