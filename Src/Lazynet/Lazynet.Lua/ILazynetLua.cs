using LuaInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LUA
{
    public interface ILazynetLua
    {
        /// <summary>
        /// 编译并运行文件
        /// </summary>
        /// <param name="filename">文件路径</param>
        object[] DoChunk(string filename);


        /// <summary>
        /// 注册实例方法
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="name">方法名</param>
        /// <param name="lname">lua方法名</param>
        /// <returns></returns>
        LuanetLuaFunction RegisterMethod(object obj, string name, string lname);


        /// <summary>
        /// 注册实例方法
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="name">方法名</param>
        /// <returns></returns>
        LuanetLuaFunction RegisterMethod(object obj, string name);


        /// <summary>
        /// 注册类方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">方法名</param>
        /// <param name="lname">lua方法名</param>
        /// <returns></returns>
        LuanetLuaFunction RegisterMethod<T>(string name, string lname);


        /// <summary>
        /// 注册类方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">方法名</param>
        /// <returns></returns>
        LuanetLuaFunction RegisterMethod<T>(string name);


        /// <summary>
        /// 获取lua函数
        /// </summary>
        /// <param name="funcName">函数名</param>
        /// <returns></returns>
        LuanetLuaFunction GetFunction(string funcName);
    }
}
