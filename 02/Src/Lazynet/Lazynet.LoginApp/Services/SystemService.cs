/*
* ==============================================================================
*
* Filename: SystemService
* Description: 
*
* Version: 1.0
* Created: 2020/5/30 11:35:49
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.AppCore;
using Lazynet.Core.Network;
using Lazynet.Core.Proto;
using Lazynet.Core.Service;
using Lazynet.Core.Util;
using Lazynet.LoginApp.Job;
using System;
using System.Collections.Generic;

namespace Lazynet.LoginApp.Services
{
    [LazynetServiceType(LazynetServiceType.System)]
    public class SystemService : LazynetBaseService
    {
        public SystemService(LazynetAppContext context)
             : base(context)
        {
        }

        [LazynetServiceAction]
        public void Connect()
        {
            SendConnectMessage(this.Context.Request.Handler);
            CreateHeartbeatJob(this.Context.Request.Handler);
        }

        /// <summary>
        /// send connect message
        /// </summary>
        /// <param name="ctx"></param>
        protected void SendConnectMessage(LazynetHandlerContext ctx)
        {
            LazynetMessage message = new LazynetMessage()
            {
                RouteUrl = EventType.Connect,
                Parameters = null
            };
            ctx.WriteAndFlushAsync(SerializeHelper.Serialize(message));
        }

        /// <summary>
        /// send heartbeat
        /// </summary>
        /// <param name="ctx"></param>
        protected void CreateHeartbeatJob(LazynetHandlerContext ctx)
        {
            //创建心跳包作业
            this.Context.Timer.AddJob<HeartbeatJob>(-1, 3, new Dictionary<string, object>() {
                { "HandlerContext", ctx}
            });
        }

        /// <summary>
        /// dis connect
        /// </summary>
        /// <param name="ctx"></param>
        [LazynetServiceAction]
        public void DisConnect()
        {
            this.Context.Timer.RemoveJob<HeartbeatJob>();
        }

        [LazynetServiceAction]
        public void Exception(Exception exception)
        {

        }

    }
}
