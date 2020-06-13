/*
* ==============================================================================
*
* Filename: LazynetLuaTable
* Description: 
*
* Version: 1.0
* Created: 2020/5/4 11:07:43
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/

using Neo.IronLua;
using System;

namespace Lazynet.Core.LUA
{
    /// <summary>
    /// lua table
    /// </summary>
    public static class LazynetLuaTable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(this LuaTable t, string key)
        {
            int value = Convert.ToInt32(t[key]);
            return value;
        }

        /// <summary>
        /// 调用函数
        /// </summary>
        /// <param name="t">表</param>
        /// <param name="methodName">方法名</param>
        /// <param name="args">参数列表</param>
        /// <returns></returns>
        public static LuaResult CallFunction(this LuaTable t, string methodName, params object[] args)
        {
            if (t.ContainsKey(methodName))
            {
                return t.CallMember(methodName, args);
            }
            return LuaResult.Empty;
        }



    }


}
