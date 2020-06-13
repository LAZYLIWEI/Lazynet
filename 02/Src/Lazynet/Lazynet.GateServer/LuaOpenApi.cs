/*
* ==============================================================================
*
* Filename: LuaOpenApi
* Description: 
*
* Version: 1.0
* Created: 2020/5/4 10:56:03
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.LUA;
using Lazynet.Core.Network;
using Lazynet.Core.Network.Server;
using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.GateServer
{
    public class LuaOpenApi
    {

        public static ILazynetSocketEvent NewSocketEvent(LuaTable parameter)
        {
            if (parameter is null)
            {
                throw new Exception("parameter is null");
            }

            return new SocketEvent(parameter);
        }

    }
}
