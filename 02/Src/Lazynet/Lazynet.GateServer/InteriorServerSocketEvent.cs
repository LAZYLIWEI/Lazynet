/*
* ==============================================================================
*
* Filename: InteriorServerSocketEvent
* Description: 
*
* Version: 1.0
* Created: 2020/5/19 0:01:04
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.GateServer
{
    public class InteriorServerSocketEvent : ILazynetSocketEvent
    {
        public void Connect(LazynetHandlerContext ctx)
        {

        }

        public void DisConnect(LazynetHandlerContext ctx)
        {

        }

        public void Exception(LazynetHandlerContext ctx, Exception exception)
        {

        }

        public void Read(LazynetHandlerContext ctx, string msg)
        {

        }
    }
}
