using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.LUA
{
    /// <summary>
    /// lua接口
    /// </summary>
    public interface ILazynetLua
    {
        void RegisterPackage(string name, Type type);
        string DoFile(string filename);
        string DoFile(string filename, string rootDirectory);
        void CallFunction(string methodName);
    }
}
