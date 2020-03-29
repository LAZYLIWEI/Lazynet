/*
* ==============================================================================
*
* Filename: LazynetLua
* Description: 
* 引用该dll需要注意你运行时的项目是否与lua51.dll同一架构(x86或x64)
* 调用方向该dll --> luainterface --> c语言的库
* 
* Version: 1.0
* Created: 2020/3/26 21:09:03
* Compiler: Visual Studio 2010
*
* Author: lazy
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
    /// 每创建一个对象就会开启一个lua虚拟机
    /// </summary>
    public class LazynetLua : ILazynetLua
    {
        public Lua Lua { get; }
        public LazynetLua()
        {
            this.Lua = new Lua();
        }

        /// <summary>
        /// 编译并运行文件
        /// </summary>
        /// <param name="filename">文件路径</param>
        public object[] DoChunk(string filename)
        {
            return this.Lua.DoFile(filename);
        }

        /// <summary>
        /// 注册实例方法
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="name">方法名</param>
        /// <param name="lname">lua方法名</param>
        /// <returns></returns>
        public LuanetLuaFunction RegisterMethod(object obj, string name, string lname)
        {
            var luafunction = Lua.RegisterFunction(lname, obj, obj.GetType().GetMethod(name));
            return new LuanetLuaFunction(luafunction);
        }



        /// <summary>
        /// 注册实例方法
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="name">方法名</param>
        /// <returns></returns>
        public LuanetLuaFunction RegisterMethod(object obj, string name)
        {
            var lazynetluaFunc = RegisterMethod(obj, name, name);
            return lazynetluaFunc;
        }

        /// <summary>
        /// 注册类方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">方法名</param>
        /// <param name="lname">lua方法名</param>
        /// <returns></returns>
        public LuanetLuaFunction RegisterMethod<T>(string name, string lname)
        {
            var luafunction = Lua.RegisterFunction(lname, null, typeof(T).GetMethod(name));
            return new LuanetLuaFunction(luafunction);
        }

        /// <summary>
        /// 注册类方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">方法名</param>
        /// <returns></returns>
        public LuanetLuaFunction RegisterMethod<T>(string name)
        {
            return RegisterMethod<T>(name, name);
        }

        /// <summary>
        /// 获取lua函数
        /// </summary>
        /// <param name="funcName">函数名</param>
        /// <returns></returns>
        public LuanetLuaFunction GetFunction(string funcName)
        {
            var luafunc = Lua.GetFunction(funcName);
            return new LuanetLuaFunction(luafunc);
        }

    }
}
