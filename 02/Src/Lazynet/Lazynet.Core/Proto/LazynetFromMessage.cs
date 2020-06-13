/*
* ==============================================================================
*
* Filename: LazynetForwardMessage
* Description: 
*
* Version: 1.0
* Created: 2020/6/1 1:04:10
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

namespace Lazynet.Core.Proto
{
    public class LazynetFromMessage : LazynetMessage
    {
        public string SessionID { get; set; }
    }
}
