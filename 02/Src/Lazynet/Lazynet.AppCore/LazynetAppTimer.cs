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
using Lazynet.Core.Util;
using Neo.IronLua;
using Quartz;
using System;
using System.Collections.Generic;

namespace Lazynet.AppCore
{
    public class LazynetAppTimer
    {
        public LazynetAppContext Context { get; }
        public Dictionary<string, ILazynetTimer> TimerDict { get; } 
        public LazynetAppTimer(LazynetAppContext context)
        {
            this.Context = context;
            this.TimerDict = new Dictionary<string, ILazynetTimer>();
        }

        public string AddJob(int repeatCount, int interval, Func<LuaTable, int> callFunction, LuaTable parameters)
        {
            string jsonString = parameters.ToJson();
            var copyParameters = LuaTable.FromJson(jsonString);
            var dict = new Dictionary<string, object>() {
                { "Callback", callFunction},
                { "Parameters", copyParameters}
            };
            string name = this.AddJob<LazynetLuaJob>(repeatCount, interval, dict);
            return name;
        }

        public string AddJob<T>(int repeatCount, int interval, IDictionary<string, object> parameters) where T : IJob
        {
            ILazynetTimer timer = new LazynetQuartz();
            var task = timer.Create<T>(repeatCount, interval, parameters);
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


        public void RemoveJob<T>() where T : IJob
        {
            var type = typeof(T);
            this.RemoveJob(type.Name);
        }
    }

}
