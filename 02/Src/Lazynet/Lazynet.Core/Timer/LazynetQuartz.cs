/*
* ==============================================================================
*
* Filename:LazynetQuartz
* Description: 
*
* Version: 1.0
* Created: 2020/5/11 17:58:11
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lazynet.Core.Timer
{
    /// <summary>
    /// Quartz.Net
    /// </summary>
    public sealed class LazynetQuartz : ILazynetTimer 
    {
        public IScheduler Scheduler { get; }

        public LazynetQuartz()
        {
            var schedulerFactory = new StdSchedulerFactory();
            Scheduler = schedulerFactory.GetScheduler().Result;
        }

        public async Task Create<T>(int repeatCount, int interval, IDictionary<string, object> parameters) where T : IJob
        {
          
            await Scheduler.Start();

            //创建作业和触发器
            var jobDetail = JobBuilder.Create<T>()
                                      .SetJobData(new JobDataMap(parameters))
                                      .Build();
            var trigger = TriggerBuilder.Create()
                                        .WithSimpleSchedule(m => {
                                            m.WithRepeatCount(repeatCount).WithIntervalInSeconds(interval);
                                        })
                                        .Build();
            //添加调度
            await Scheduler.ScheduleJob(jobDetail, trigger);
        }

        public async Task Create<T>(int repeatCount, int interval) where T : IJob
        {
            await Scheduler.Start();

            //创建作业和触发器
            var jobDetail = JobBuilder.Create<T>()
                                      .Build();
            var trigger = TriggerBuilder.Create()
                                        .WithSimpleSchedule(m => {
                                            m.WithRepeatCount(repeatCount).WithIntervalInSeconds(interval);
                                        })
                                        .Build();
            //添加调度
            await Scheduler.ScheduleJob(jobDetail, trigger);
        }

        public void Destroy()
        {
            this.Scheduler.Shutdown();
        }

    }
}
