/*
* ==============================================================================
*
* Filename: LazynetMessageEntity
* Description: 
*
* Version: 1.0
* Created: 2020/3/22 13:48:25
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
    /// <summary>
    /// 消息实体
    /// </summary>
    public class LazynetServiceMessage
    {
        public LazynetMessageType Type { get; }
        public string RouteUrl { get; }
        public object[] Parameters { get; }
        public LazynetServiceMessage(LazynetMessageType type, string routeUrl, object[] parameters)
        {
            this.Type = type;
            this.RouteUrl = routeUrl;
            this.Parameters = parameters;
        }
    }
}
