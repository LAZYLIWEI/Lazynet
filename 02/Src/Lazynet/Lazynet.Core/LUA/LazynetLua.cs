/*
* ==============================================================================
*
* Filename: LazynetLua
* Description: 
*
* Version: 1.0
* Created: 2020/5/3 22:24:12
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
using System.IO;
using System.Text;

namespace Lazynet.Core.LUA
{
    public class LazynetLua : ILazynetLua
    {
        public LuaGlobal G { get; }


        public LazynetLua()
        {
            using (Lua l = new Lua())
            {
                this.G = l.CreateEnvironment();
            }
        }

        public void RegisterPackage(string name, Type type)
        {
            this.G.RegisterPackage(name, type);
        }

        public string DoFile(string filename)
        {
            string result = string.Empty;
            try
            {
                var chunkResult = this.G.DoChunk(filename);
                result = chunkResult.ToString();
            }
            catch
            {
                throw new Exception("load lua file failed");
            }

            return result;
        }


        public string DoFile(string filename, string directory)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(currentDirectory + directory);
            string result = this.DoFile(filename);
            Directory.SetCurrentDirectory(currentDirectory);
            return result;
        }


        public void RegisterMethod(string methodName, Delegate method, bool ignoreCase = false)
        {
            this.G.DefineMethod(methodName, method, ignoreCase);
        }

        public void RegisterFunction(string methodName, Delegate method, bool ignoreCase = false)
        {
            this.G.DefineFunction(methodName, method, ignoreCase);
        }

        public void CallFunction(string methodName)
        {
            this.G.CallMember(methodName);
        }

       
    }
}
