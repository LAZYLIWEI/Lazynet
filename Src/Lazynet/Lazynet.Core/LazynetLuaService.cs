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

        public void ExitApi()
        {
            this.Interrupt();
        }

        public void KillApi(int ID)
        {
            this.Kill(ID);
        }

        public string GetAliasApi()
        {
            return this.Alias;
        }
      
        public void SetAliasApi(string alias)
        {
            base.SetAlias(alias);
        }

        public override object[] Start()
        {
            base.Start();
            var luaResult = this.Lua.DoChunk(this.Filename);
            return luaResult;
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

        public void StartServiceApi(int ID)
        {
            this.StartService(ID);
        }
        #endregion

        #region message
        public void SendLuaMessageApi(int serviceID, string cmd, LuaTable table)
        {
            List<object> paramters = new List<object>();
            foreach (var item in table.Keys)
            {
                paramters.Add(table[item]);
            }
            this.SendMessage(serviceID, new LazynetServiceMessage(LazynetMessageType.Lua, cmd, paramters.ToArray()));
        }
        #endregion

        #region net
        public void CreateLuaSocketApi(int port, int heartbeat, int type)
        {
            this.CreateSocket(new LazynetSocketConfig() {
                Heartbeat = heartbeat,
                Port = port,
                Type = (LazynetSocketType)type
            });
        }

        public void BindAsyncApi(string activeEvent, string inactiveEvent, string readEvent, string exceptionEvent)
        {
            LazynetSocketEvent socketEvent = new LazynetSocketEvent()
            {
                ActiveEvent = activeEvent,
                ExceptionEvent = exceptionEvent,
                InactiveEvent = inactiveEvent,
                ReadEvent = readEvent
            };
            if (this.Socket is null)
            {
                throw new Exception("请先create socket, 再调用此方法");
            }

            this.SocketEvent = socketEvent;
            this.Socket.SetEvent(new LazynetDefaultSocketEvent(this));
            this.Socket.BindAsync();
        }
        #endregion

        #region log
        public void LogApi(string str)
        {
            this.Context.Logger.Info(this.ID.ToString(), str);
        }
        #endregion

        #region session
        public void SetSessionGroupApi(ILazynetSessionGroup sessionGroup)
        {
            if (sessionGroup != null)
            {
                this.SetSessionGroup(sessionGroup);
            }
        }

        public void AddSessionApi(LazynetChannelHandlerContext ctx)
        {
            string ID = SnowflakeUtil.Instance().GetString();
            this.SessionGroup.Add(new LazynetSession() { 
                 ID = ID,
                 Context = ctx
            });
        }

        public void RemoveSessionApi(string ID)
        {
            RemoveSession(ID);
        }

        public void ClearSessionApi()
        {
            this.ClearSession();
        }

        public object[] FindSessionApi(string ID)
        {
            var result = new object[2];
            var session = this.FindSession(ID);
            if (session != null)
            {
                result[0] = session.ID;
                result[1] = session.Context;
            }
            return result;
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
                { "SendLuaMessageApi", "sendMessage"},
                { "AddLuaTriggerApi", "addTrigger"},
                { "RemoveLuaTriggerApi", "removeTrigger"},
                { "ExitApi", "exit"},
                { "KillApi", "kill"},
                { "CreateLuaSocketApi", "createSocket"},
                { "StartServiceApi", "startService"},
                { "BindAsyncApi", "bind"},
                { "LogApi", "log"},
                { "SetSessionGroupApi", "setSessionGroup"},
                { "AddSessionApi", "addSession"},
                { "RemoveSessionApi", "removeSession"},
                { "ClearSessionApi", "clearSession"},
                { "FindSession", "findSession"}
            };
            foreach (var item in methodDict)
            {
                this.Lua.RegisterMethod(this, item.Key, item.Value);
            }

            Dictionary<string, string> functionDict = new Dictionary<string, string>() {
                { "WriteApi", "write"},
                { "WriteAndFlushApi", "writeAndFlush"},
            };
            foreach (var item in functionDict)
            {
                this.Lua.RegisterMethod<LazynetLuaApi>(item.Key, item.Value);
            }
        }

        #endregion

    }
}
