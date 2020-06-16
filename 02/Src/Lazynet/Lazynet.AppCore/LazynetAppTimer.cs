/*
* ==============================================================================
*
* Filename: LazynetAppTimer
* Description: 
*
* Version: 1.0
* Created: 2020/5/30 17:10:40
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Timer;
using Quartz;
using System.Collections.Generic;

namespace Lazynet.AppCore
{
    public class LazynetAppTimer
    {
        public Dictionary<string, ILazynetTimer> TimerDict { get; } 
        public LazynetAppTimer()
        {
            this.TimerDict = new Dictionary<string, ILazynetTimer>();
        }

        public void AddJob<T>(string name, int repeatCount, int interval, IDictionary<string, object> parameters) where T : IJob
        {
            ILazynetTimer timer = new LazynetQuartz();
            timer.Create<T>(repeatCount, interval, parameters);
            this.TimerDict.Add(name, timer);
        }

        public void AddJob<T>(int repeatCount, int interval, IDictionary<string, object> parameters) where T : IJob
        {
            var type = typeof(T);
            AddJob<T>(type.Name, repeatCount, interval, parameters);
        }

        public void RemoveJob(string name)
        {
            if (this.TimerDict.ContainsKey(name))
            {
                var timer = this.TimerDict[name];
                timer.Destroy();
            }
        }

        public void RemoveJob<T>() where T : IJob
        {
            var type = typeof(T);
            RemoveJob(type.Name);
        }
    }

}
