/*
* ==============================================================================
*
* Filename: LazynetLuaService
* Description: 
*
* Version: 1.0
* Created: 2020/3/25 21:47:32
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.LUA;
using Lazynet.Network;
using Lazynet.Util;
using LuaInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core
{
    public class LazynetLuaService : LazynetService
    {
        public ILazynetLua Lua { get; }
        public string Filename { get; }
        public LazynetLuaService(ILazynetContext context, string filename)
            : base(context)
        {
            this.Filename = filename;
            this.Lua = new LazynetLua();
            this.OpenLuaApi();
        }

        #region trigger
        public void AddLuaTriggerApi(string command, LuaFunction func)
        {
            LazynetLuaTrigger trigger = new LazynetLuaTrigger()
            {
                Command = command,
                Function = func
            };
            this.AddTrigger(command, trigger);
        }

        public bool RemoveLuaTriggerApi(string command)
        {
            return RemoveTrigger(command);
        }
        #endregion

        #region service
        public int GetServiceID(string alias)
        {
            return this.Context.GetServiceID(alias);
        }

        public int GetIDApi()
        {
            return this.ID;
        }

        public string GetAliasApi()
        {
            return this.Alias;
        }

        public void SetAliasApi(string alias)
        {
            base.SetAlias(alias);
        }

        public override void Start()
        {
            this.Start(() =>
            {
                this.Lua.DoChunk(this.Filename);
            });
        }

        public int CreateLuaServiceApi(string filename)
        {
            var luaService = this.Context.CreateLuaService(filename);
            if (luaService is null)
            {
                throw new Exception("创建服务失败");
            }
            return luaService.ID;
        }

        #endregion

        #region message
        public void SendMessageApi(int serviceID, string cmd, LuaTable table)
        {
            List<object> paramters = new List<object>();
            foreach (var item in table.Keys)
            {
                paramters.Add(table[item]);
            }
            this.SendMessage(serviceID, new LazynetServiceMessage(LazynetMessageType.Normal, cmd, paramters.ToArray()));
        }

        public void SendSystemMessageApi(int serviceID, string cmd, LuaTable table)
        {
            List<object> paramters = new List<object>();
            foreach (var item in table.Keys)
            {
                paramters.Add(table[item]);
            }
            this.SendMessage(serviceID, new LazynetServiceMessage(LazynetMessageType.System, cmd, paramters.ToArray()));
        }

        #endregion

        #region lua method
        private void OpenLuaApi()
        {
            Dictionary<string, string> methodDict = new Dictionary<string, string>() {
                { "GetAliasApi", "getAlias"},
                { "SetAliasApi", "setAlias"},
                { "GetIDApi", "getID"},
                { "GetServiceID", "getServiceID"},
                { "CreateLuaServiceApi", "createService"},
                { "SendMessageApi", "sendMessage"},
                { "SendSystemMessageApi", "sendSystemMessage"},
                { "AddLuaTriggerApi", "addTrigger"},
                { "RemoveLuaTriggerApi", "removeTrigger"},
            };
            foreach (var item in methodDict)
            {
                this.Lua.RegisterMethod(this, item.Key, item.Value);
            }
        }
        #endregion
    }
}
