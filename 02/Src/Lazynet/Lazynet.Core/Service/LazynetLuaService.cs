using Lazynet.Core.LUA;
using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Service
{
    public class LazynetLuaService : ILazynetService
    {
        public LazynetServiceType Type { get; set; }
        public string Command { get; set; }
        public LuaTable Table { get; set; }

        public object[] CallBack(LazynetServiceEntity message)
        {
            var result = this.Table.CallMember(this.Command, message.Parameters);
            return result.Values;
        }
    }
}
