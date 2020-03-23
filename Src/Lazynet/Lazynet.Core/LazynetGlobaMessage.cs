/*
* ==============================================================================
*
* Filename: LazynetGlobaMessage
* Description: 
*
* Version: 1.0
* Created: 2020/3/22 18:54:08
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
    public class LazynetGlobaMessage
    {
        public int ServiceID { get; set; }
        public LazynetServiceMessage ServiceMessage { get; set; }
    }
}
