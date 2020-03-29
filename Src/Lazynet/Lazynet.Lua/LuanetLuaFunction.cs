/*
* ==============================================================================
*
* Filename: LuanetLuaFunction
* Description: 
*
* Version: 1.0
* Created: 2020/3/26 21:20:36
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using LuaInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LUA
{
    /// <summary>
    /// lua函数
    /// </summary>
    public class LuanetLuaFunction 
    {
        public LuaFunction LuaFunc { get; }
        public LuanetLuaFunction(LuaFunction func)
        {
            this.LuaFunc = func;
        }

        public object[] Call(params object[] args)
        {
            return this.LuaFunc.Call(args);
        }
    }
}
