/*
* ==============================================================================
*
* Filename: LazynetOpenApiLoadder
* Description: 
*
* Version: 1.0
* Created: 2020/5/4 11:48:36
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.LUA;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LuaCore
{
    public class LazynetOpenApiLoadder
    {

        public static ILazynetLua Load(ILazynetLua lua)
        {
            lua.RegisterPackage("lazynet", typeof(LazynetOpenApi));
            return lua;
        }

    }
}
