/*
* ==============================================================================
*
* Filename: HeartbeatJob
* Description: 
*
* Version: 1.0
* Created: 2020/5/24 9:15:10
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
using Lazynet.Core.Util;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lazynet.LoginApp.Job
{
    /// <summary>
    /// 心跳包任务
    /// </summary>
    public class HeartbeatJob : IJob
    {
        public LazynetHandlerContext HandlerContext { get; set; }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(3000);
                LazynetMessage message = new LazynetMessage()
                {
                    RouteUrl = EventType.Heartbeat,
                    Parameters = null
                };
                HandlerContext.WriteAndFlushAsync(SerializeHelper.Serialize(message));
            });
        }

    }
}
