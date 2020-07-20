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
using Lazynet.Core.Action;
using Lazynet.Core.Util;
using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Text;
using Lazynet.Core.Logger;

namespace Lazynet.AppCore
{
    public class LazynetAppService
    {
        public LazynetLuaActionManager ActionManager { get; }
        public ILazynetAppContext Context { get; }

        public LazynetAppService(ILazynetAppContext context)
        {
            this.ActionManager = new LazynetLuaActionManager();
            this.Context = context;
        }

        public ILazynetAction Get(string name)
        {
            return this.ActionManager.Get(name);
        }
     
        public void AddService(LuaTable table)
        {
            this.ActionManager.AddArray(table);
        }

        public object[] CallService(LazynetMessage message)
        {
            if (message == null)
            {
                this.Context.Log(LazynetLogLevel.Warn, " message is null ");
                return null;
            }
            var service = this.Get(message.RouteUrl);
            if (service == null)
            {
                this.Context.Log(LazynetLogLevel.Warn, " don't have the serivce, route url: " + message.RouteUrl);
                return null;
            }
            var result = this.ActionManager.Call(message.RouteUrl, message.Parameters?.ToArray());
            return result;
        }
    }
}
