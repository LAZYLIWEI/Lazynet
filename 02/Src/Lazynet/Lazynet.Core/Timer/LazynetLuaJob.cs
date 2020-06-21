/*
* ==============================================================================
*
* Filename: LazynetLuaJob
* Description: 
*
* Version: 1.0
* Created: 2020/6/21 17:58:14
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
using System.Threading.Tasks;

namespace Lazynet.Core.Timer
{
    public class LazynetLuaJob : IJob
    {
        public Func<LuaTable, int> Callback { get; set; }
        public LuaTable Parameters { get; set; }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                int result = this.Callback(this.Parameters);
                return result;
            });
        }

    }
}
