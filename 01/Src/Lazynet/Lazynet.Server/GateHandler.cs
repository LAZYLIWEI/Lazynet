/*
* ==============================================================================
*
* Filename:GateHandler
* Description: 
*
* Version: 1.0
* Created: 2020/3/22 19:41:55
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Server
{
    public class GateHandler : LazynetTriggerProvider
    {
        public GateHandler(ILazynetService serviceContext)
            : base(serviceContext)
        {
        }

        public void PrintHelloWorld()
        {
            Console.WriteLine("HelloWorld");
        }

        public void KillService()
        {
            this.ServiceContext.Interrupt();
        }

        public void Say(double id, string content)
        {
            Console.WriteLine(id + " say " + this.ServiceContext.ID + content);
        }

    }
}
