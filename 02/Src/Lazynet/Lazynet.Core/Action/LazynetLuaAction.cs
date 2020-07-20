/*
* ==============================================================================
*
* Filename: LazynetLuaAction
* Description: 
*
* Version: 1.0
* Created: 2020/7/20 23:18:20
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

namespace Lazynet.Core.Action
{
    public class LazynetLuaAction : ILazynetAction
    {
        public string MemberName { get; }
        public LuaTable MethodTable { get; }

        public LazynetLuaAction(string memberName, LuaTable methodTable)
        {
            this.MemberName = memberName;
            this.MethodTable = methodTable;
        }

        public object[] Call(object[] parameterArray)
        {
            var result = this.MethodTable.CallMember(this.MemberName, parameterArray);
            return result;
        }

    }
}
