/*
* ==============================================================================
*
* Filename: SystemServiceAttribute
* Description: 
*
* Version: 1.0
* Created: 2020/5/30 22:19:23
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

namespace Lazynet.Core.Service
{
    public class LazynetServiceTypeAttribute : Attribute
    {
        public LazynetServiceType Type { get; }
        public LazynetServiceTypeAttribute()
            : this(LazynetServiceType.Normal)
        {
        }
        public LazynetServiceTypeAttribute(LazynetServiceType type)
        {
            this.Type = type;
        }
    }
}
