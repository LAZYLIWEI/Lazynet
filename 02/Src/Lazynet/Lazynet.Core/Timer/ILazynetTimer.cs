/*
* ==============================================================================
*
* Filename: ILazynetTimer
* Description: 
*
* Version: 1.0
* Created: 2020/5/11 17:56:56
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lazynet.Core.Timer
{
    /// <summary>
    /// 定时器任务组件
    /// </summary>
    public interface ILazynetTimer
    {
        Task<string> Create<T>(int repeatCount, int interval, IDictionary<string, object> parameters) where T : IJob;
        Task<string> Create<T>(int repeatCount, int interval) where T : IJob;
        void Remove(string name);
        void Pause(string name);
        void Resume(string name);
        void Destroy();
    }
}
