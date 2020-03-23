/*
* ==============================================================================
*
* Filename: LazynetLua
* Description: 
*
* Version: 1.0
* Created: 2020/3/23 21:06:08
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core
{
    public class LazynetLua
    {
        public LuaGlobal G { get; } 
        public LazynetLua()
        {
            var lua = new Lua();
            this.G = lua.CreateEnvironment();
        }

        public LuaResult DoChunk(string fileName, params KeyValuePair<string, object>[] args)
        {
            return this.G.DoChunk(fileName, args);
        }


        ~LazynetLua()
        {
            Console.WriteLine("释放lua");
            this.G.Lua.Dispose();
        }

    }
}
