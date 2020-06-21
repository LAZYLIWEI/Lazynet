/*
* ==============================================================================
*
* Filename: AppContext
* Description: 
*
* Version: 1.0
* Created: 2020/5/30 1:30:55
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Cache;
using Lazynet.Core.Logger;
using Lazynet.Core.LUA;
using Lazynet.Core.Network;
using Lazynet.Core.Proto;
using Lazynet.Core.Service;
using Lazynet.Core.Timer;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.AppCore
{
    public class LazynetAppContext
    {
        public LazynetAppConfig Config { get; set; }
        public ILazynetLogger Logger { get; set; }
        public ILazynetCache<string> Cache { get; set; }
        public LazynetAppService Service { get; set; }
        public LazynetAppServer Server { get; set; }
        public LazynetAppTimer Timer{ get; set; }
        public LazynetAppFilter AppFilter { get; set; }
        public ILazynetLua Lua { get; set; }
        public LazynetServiceRequest Request { get; set; }


        public void Response(string ID, object result)
        {
            LazynetBackMessage message = new LazynetBackMessage()
            {
                RouteUrl = this.Request.RouteUrl,
                Parameter = result
            };
            this.Wrap(ID, SerializeHelper.Serialize(message));
        }

        private void Wrap(string ID, string msg)
        {
            LazynetMessage message = new LazynetMessage()
            {
                RouteUrl = LazynetActionConstant.Dispatch,
                Parameters = new List<object> {
                    ID,
                    msg
                }
            };
            this.Request.Handler.WriteAndFlushAsync(SerializeHelper.Serialize(message));
        }


    }
}
