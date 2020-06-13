/*
* ==============================================================================
*
* Filename: LazynetSharpTrigger
* Description: 
*
* Version: 1.0
* Created: 2020/5/5 9:44:39
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
    public class LazynetSharpService : ILazynetService 
    {
        public LazynetServiceType Type { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public object Instance { get; set; }

        public LazynetSharpService(LazynetServiceType type, object instance, MethodInfo methodInfo)
        {
            this.Type = type;
            this.Instance = instance;
            this.MethodInfo = methodInfo;
        }

        public object[] CallBack(LazynetServiceEntity message)
        {
            var objectArray = new object[1];
            if (this.Type == message.Type)
            {
                var result = this.MethodInfo?.CallMethod(this.Instance, message.Parameters);
                objectArray[0] = result;
            }
            return objectArray;
        }

    }
}
