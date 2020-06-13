/*
* ==============================================================================
*
* Filename: LazynetServerOpenApi
* Description: 
*
* Version: 1.0
* Created: 2020/5/4 11:30:26
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.LUA;
using Lazynet.Core.Network;
using Lazynet.Core.Network.Client;
using Lazynet.Core.Network.Server;
using Lazynet.Core.Service;
using Lazynet.Core.Timer;
using Lazynet.Core.Util;
using Neo.IronLua;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lazynet.LuaCore
{
    public class LazynetOpenApi
    {
        #region network
        public static ILazynetServer NewServer(NewServerParameter paramter)
        {
            if (paramter is null)
            {
                throw new Exception("parameter is null");
            }

            ILazynetServer server = new LazynetServer(new LazynetServerConfig()
            {
                Port = paramter.port,
                Heartbeat = paramter.heartbeat,
                SocketType = paramter.socketType,
                WebsocketPath = paramter.websocketPath
            });
            return server;
        }

        public static void SetServerSocketEvent(
            ILazynetServer server,
            ILazynetSocketEvent socketEvent)
        {
            if (server == null
                || socketEvent == null)
            {
                throw new Exception("server为nul或者event为null");
            }

            server.SetSocketEvent(socketEvent);
        }

        public static void Bind(ILazynetServer server)
        {
            if (server == null)
            {
                throw new Exception("server为nul或者event为null");
            }

            server.Bind();
        }

        public static void CloseServer(ILazynetServer server)
        {
            if (server == null)
            {
                throw new Exception("server为nul或者event为null");
            }

            server.Close();
        }

        public static string GetAddress(LazynetHandlerContext context)
        {
            if (context is null)
            {
                throw new Exception("context is null");
            }

            return context.GetAddress();
        }


        public static void WriteAndFlush(LazynetHandlerContext context, string msg)
        {
            if (context is null)
            {
                throw new Exception("context is null");
            }

            context.WriteAndFlushAsync(msg);
        }

        public static void Write(LazynetHandlerContext context, string msg)
        {
            if (context is null)
            {
                throw new Exception("context is null");
            }

            context.WriteAsync(msg);
        }


        public static void CloseConnect(LazynetHandlerContext context)
        {
            if (context is null)
            {
                throw new Exception("context is null");
            }

            context.CloseAsync();
        }


        public static ILazynetClient NewClient(NewClientParameter paramter)
        {
            if (paramter is null)
            {
                throw new Exception("parameter is null");
            }

            ILazynetClient client = new LazynetClient(new LazynetClientConfig()
            {
                IP = paramter.ip,
                Port = paramter.port,
                SocketType = paramter.socketType,
                WebsocketPath = paramter.websocketPath
            });
            return client;
        }

        public static void SetClientSocketEvent(
            ILazynetClient client,
            ILazynetSocketEvent socketEvent)
        {
            if (client == null
                || socketEvent == null)
            {
                throw new Exception("client is nul or event is null");
            }

            client.SetSocketEvent(socketEvent);
        }

        public static bool ConnectToHost(ILazynetClient client)
        {
            if (client == null)
            {
                throw new Exception("client为nul");
            }

            return client.ConnectToHost();
        }
        #endregion

        #region trigger
        public static Dictionary<string, ILazynetService> NewService()
        {
            Dictionary<string, ILazynetService> dict = new Dictionary<string, ILazynetService>();
            return dict;
        }

        public static void AddService(Dictionary<string, ILazynetService> serviceDict, LuaTable table)
        {
            if (serviceDict is null)
            {
                throw new Exception("service is null");
            }
            if (table is null)
            {
                throw new Exception("table is null");
            }

            foreach (var item in table.Members)
            {
                ILazynetService service = new LazynetLuaService()
                {
                    Command = item.Key,
                    Table = table
                };
                if (serviceDict.ContainsKey(item.Key))
                {
                    serviceDict[item.Key] = service;
                }
                else
                {
                    serviceDict.Add(item.Key, service);
                }
            }
        }

        public static void RemoveTrigger(Dictionary<string, ILazynetService> triggers, string cmd)
        {
            if (triggers is null)
            {
                throw new Exception("triggers is null");
            }
            if (triggers.ContainsKey(cmd))
            {
                triggers.Remove(cmd);
            }
        }

        public static void CallService(Dictionary<string, ILazynetService> triggers, string cmd, object[] parameters)
        {
            if (triggers is null)
            {
                throw new Exception("service is null");
            }
            if (triggers.ContainsKey(cmd))
            {
                var trigger = triggers[cmd];
                trigger.CallBack(new LazynetServiceEntity()
                {
                    Type = LazynetServiceType.Normal,
                    RouteUrl = cmd,
                    Parameters = parameters
                });
            }
        }
        #endregion

        #region encrypt

        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DESEncrypt(string input, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("key is null or empty");
            }

            return EncryptionHelper.DESEncrypt(input, key);
        }


        /// <summary>
        /// DES 解密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DESDecrypt(string input, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("key is null or empty");
            }

            return EncryptionHelper.DESDecrypt(input, key);
        }

        #endregion

        #region thread
        public static bool CreateThread(LuaTable handle)
        {
            if (handle == null)
            {
                return false;
            }

            bool result = ThreadPool.QueueUserWorkItem((state) =>
            {
                foreach (var item in handle.Members)
                {
                    handle.CallMember(item.Key);
                }
            });
            return result;
        }

        #endregion


    }
}
