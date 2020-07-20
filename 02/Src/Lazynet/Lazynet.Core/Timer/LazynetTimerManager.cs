/*
* ==============================================================================
*
* Filename: LazynetTimerManager
* Description: 
*
* Version: 1.0
* Created: 2020/7/21 0:38:17
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Neo.IronLua;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Timer
{
    public class LazynetTimerManager
    {
        public Dictionary<string, ILazynetTimer> TimerDict { get; }
        public LazynetTimerManager()
        {
            this.TimerDict = new Dictionary<string, ILazynetTimer>();
        }

        public string AddJob(int repeatCount, 
            int interval, Func<LuaTable, int> call, 
            LuaTable parameters)
        {
            string jsonString = parameters.ToJson();
            var luaParameter = LuaTable.FromJson(jsonString);
            var dict = new Dictionary<string, object>() {
                { "Callback", call},
                { "Parameters", luaParameter}
            };

            ILazynetTimer timer = new LazynetQuartz();
            var task = timer.Create<LazynetLuaJob>(repeatCount, interval, dict);
            this.TimerDict.Add(task.Result, timer);
            return task.Result;
        }

        public string AddJob(int repeatCount, 
            int interval, 
            IDictionary<string, object> parameters) 
        {
            ILazynetTimer timer = new LazynetQuartz();
            var task = timer.Create<IJob>(repeatCount, interval, parameters);
            this.TimerDict.Add(task.Result, timer);
            return task.Result;
        }

        public void RemoveJob(string name)
        {
            if (this.TimerDict.ContainsKey(name))
            {
                var timer = this.TimerDict[name];
                timer.Remove(name);
                this.TimerDict.Remove(name);
            }
        }

        public void PauseJob(string name)
        {
            if (this.TimerDict.ContainsKey(name))
            {
                var timer = this.TimerDict[name];
                timer.Pause(name);
            }
        }

        public void ResumeJob(string name)
        {
            if (this.TimerDict.ContainsKey(name))
            {
                var timer = this.TimerDict[name];
                timer.Resume(name);
            }
        }

        public void RemoveJob()
        {
            var type = typeof(IJob);
            this.RemoveJob(type.Name);
        }
    }
}
