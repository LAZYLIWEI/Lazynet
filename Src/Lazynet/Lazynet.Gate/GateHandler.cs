/*
* ==============================================================================
*
* Filename: Gate
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

namespace Lazynet.Gate
{
    public class GateHandler
    {
        public void PrintHelloWorld(LazynetServiceContext context)
        {
            Console.WriteLine("HelloWorld");
        }

        public void KillService(LazynetServiceContext context)
        {
            context.Interrupt();
        }

    }
}
