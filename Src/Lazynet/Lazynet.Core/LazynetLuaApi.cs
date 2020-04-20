/*
* ==============================================================================
*
* Filename: LazynetLuaApi
* Description: 
*
* Version: 1.0
* Created: 2020/4/18 20:06:51
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core
{
    public class LazynetLuaApi
    {
        public static void WriteApi(LazynetChannelHandlerContext ctx, string message)
        {
            ctx?.WriteAsync(message);
        }

        public static void WriteAndFlushApi(LazynetChannelHandlerContext ctx, string message)
        {
            ctx?.WriteAndFlushAsync(message);
        }
    }
}
