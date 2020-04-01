/*
* ==============================================================================
*
* Filename: LazynetSharpService
* Description: 
*
* Version: 1.0
* Created: 2020/3/29 19:25:43
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core
{
    /// <summary>
    /// c#服务
    /// </summary>
    public class LazynetSharpService : LazynetService
    {
        public LazynetSharpService(ILazynetContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="serviceID">服务号</param>
        /// <param name="message">消息实体</param>
        /// <returns></returns>
        public LazynetService SendSharpMessage(int serviceID, string routeUrl, object[] parameters)
        {
            LazynetServiceMessage message = new LazynetServiceMessage(LazynetMessageType.Sharp, routeUrl, parameters);
            this.Context.RecvMessage(serviceID, message);
            return this;
        }
    }
}
