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
            this.RegisterMethod();
        }

        #region trigger
        /// <summary>
        /// 添加lua触发器
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="func">函数</param>
        public void AddLuaTrigger(string command, LuaFunction func)
        {
            LazynetLuaTrigger trigger = new LazynetLuaTrigger() {
                Command = command,
                Function = func
            };
            this.AddTrigger(command, trigger);
        }

        #endregion


        #region service
        /// <summary>
        /// 根据别名获取服务号
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public int GetServiceID(string alias)
        {
            return this.Context.GetServiceID(alias);
        }

        /// <summary>
        /// 获取id
        /// </summary>
        /// <returns></returns>
        public int GetID()
        {
            return this.ID;
        }


        /// <summary>
        /// 退出
        /// </summary>
        public void Exit()
        {
            this.Interrupt();
        }

        /// <summary>
        /// 获取alias
        /// </summary>
        /// <returns></returns>
        public string GetAlias()
        {
            return this.Alias;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        public new object[] Start()
        {
            base.Start();
            var luaResult = this.Lua.DoChunk(this.Filename);
            return luaResult;
        }

        /// <summary>
        /// 创建lua服务
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public int CreateLuaService(string filename)
        {
            var luaService = this.Context.CreateLuaService(filename);
            if (luaService is null)
            {
                throw new Exception("创建服务失败");
            }

            luaService.Start();
            return luaService.GetID();
        }
        #endregion


        #region message
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="serviceID">服务号</param>
        /// <param name="cmd">命令</param>
        /// <param name="table">表</param>
        public void SendLuaMessage(int serviceID, string cmd, LuaTable table)
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
        public void CreateLuaSocket()
        {
            base.CreateSocket();
            this.Socket.SetEvent(new LazynetDefaultSocketEvent(this));
        }
        #endregion


        #region lua method
        /// <summary>
        /// 注册方法给lua
        /// </summary>
        private void RegisterMethod()
        {
            Dictionary<string, string> methodDict = new Dictionary<string, string>() {
                { "GetAlias", "getAlias"},
                { "SetAlias", "setAlias"},
                { "GetID", "getID"},
                { "GetServiceID", "getServiceID"},
                { "CreateLuaService", "createService"},
                { "SendLuaMessage", "sendMessage"},
                { "AddLuaTrigger", "addTrigger"},
                { "RemoveTrigger", "removeTrigger"},
                { "Exit", "exit"},
                { "CreateLuaSocket", "createSocket"},
                { "BindAsync", "bindAsync"},
                { "CloseSocket", "closeSocket"},
            };

            foreach (var item in methodDict)
            {
                this.Lua.RegisterMethod(this, item.Key, item.Value);
            }
        }
        #endregion

    }
}
