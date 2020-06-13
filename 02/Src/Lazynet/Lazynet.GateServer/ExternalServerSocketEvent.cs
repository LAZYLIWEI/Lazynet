/*
* ==============================================================================
*
* Filename: ExternalServerSocketEvent
* Description: 
*
* Version: 1.0
* Created: 2020/5/19 0:04:48
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
    public class ExternalServerSocketEvent : ILazynetSocketEvent
    {
        public void Connect(LazynetHandlerContext ctx)
        {
            throw new NotImplementedException();
        }

        public void DisConnect(LazynetHandlerContext ctx)
        {
            throw new NotImplementedException();
        }

        public void Exception(LazynetHandlerContext ctx, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Read(LazynetHandlerContext ctx, string msg)
        {
            throw new NotImplementedException();
        }
    }



}
