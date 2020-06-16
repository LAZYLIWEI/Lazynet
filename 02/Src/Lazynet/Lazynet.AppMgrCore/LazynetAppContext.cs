/*
* ==============================================================================
*
* Filename: LazynetAppContext
* Description: 
*
* Version: 1.0
* Created: 2020/6/13 10:06:45
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.AppMgrCore
{
    public class LazynetAppContext
    {
        public LazynetAppConfig Config { get; set; }
        public ILazynetLogger Logger { get; set; }
        public LazynetAppTimer Timer { get; set; }
        public LazynetAppFilter AppFilter { get; set; }
        public LazynetExternalServer ExternalServer { get; set; }
        public LazynetInteriorServer InteriorServer { get; set; }

    }
}
