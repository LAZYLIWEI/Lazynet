/*
* ==============================================================================
*
* Filename: AppConfig
* Description: 
*
* Version: 1.0
* Created: 2020/5/29 22:52:23
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Network.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.AppCore
{
    /// <summary>
    /// app config
    /// </summary>
    public class LazynetAppConfig
    {
        public string RedisHost { get; set; }
        public string RedisPassword { get; set; }
        public LazynetClientConfig NetworkConfig { get; set; }
    }
}
