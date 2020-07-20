/*
* ==============================================================================
*
* Filename: LazynetSharpAction
* Description: 
*
* Version: 1.0
* Created: 2020/7/20 23:12:08
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

namespace Lazynet.Core.Action
{
    public class LazynetSharpAction : ILazynetAction
    {
        public MethodInfo MethodInfo { get; }
        public object Instance { get; }

        public LazynetSharpAction(object instance, MethodInfo methodInfo)
        {
            this.Instance = instance;
            this.MethodInfo = methodInfo;
        }

        public object[] Call(object[] parameterArray)
        {
            var result = this.MethodInfo.Invoke(this.Instance, parameterArray);
            return new object[] {
                result
            };
        }

    }
}
