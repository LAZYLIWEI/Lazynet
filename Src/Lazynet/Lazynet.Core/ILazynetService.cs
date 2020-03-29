/*
* ==============================================================================
*
* Filename:ILazynetService
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
using Lazynet.LUA;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lazynet.Core
{
    /// <summary>
    /// lazynet服务接口
    /// </summary>
    public interface ILazynetService
    {
        ILazynetContext Context { get; }
        int ID { get; }
        void Interrupt();
        void Start();
    }
}
