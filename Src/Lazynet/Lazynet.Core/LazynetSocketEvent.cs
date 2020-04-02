/*
* ==============================================================================
*
* Filename: LazynetSocketEvent
* Description: 
*
* Version: 1.0
* Created: 2020/4/2 23:33:31
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

namespace Lazynet.Core
{
    public class LazynetSocketEvent
    {
        public string ReadEvent { get; set; }
        public string InactiveEvent { get; set; }
        public string ExceptionEvent { get; set; }
        public string ActiveEvent { get; set; }
    }
}
