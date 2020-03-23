/*
* ==============================================================================
*
* Filename: LazynetServiceContext
* Description: 
*
* Version: 1.0
* Created: 2020/3/22 14:05:51
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lazynet.Core
{
    /// <summary>
    /// lazynet服务上下文
    /// </summary>
    public class LazynetServiceContext
    {
        public int ID { get; set; }
        public Thread ThreadHandle { get; set; }
        public string Name { get; set; }
        public Queue<LazynetServiceMessage> MessageQueue { get; set; }
        public ManualResetEvent ManualEvent { get; set; }
        public LazynetServiceState State { get; set; }
        public LazynetLua Lua { get; set; }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="obj"></param>
        public void Start(object obj)
        {
            this.ThreadHandle.Start(obj);
        }


        public void SetState(LazynetServiceState state)
        {
            lock(this)
            {
                this.State = state;
            }
        }


        /// <summary>
        /// 中断服务
        /// </summary>
        public void Interrupt()
        {
            this.SetState(LazynetServiceState.Exit);
        }

    }
}
