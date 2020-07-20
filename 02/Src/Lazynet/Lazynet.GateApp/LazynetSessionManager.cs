/*
* ==============================================================================
*
* Filename: LazynetSessionManager
* Description: 
*
* Version: 1.0
* Created: 2020/7/11 22:17:52
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Network;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.GateApp
{
    public class LazynetSessionManager
    {
        public Dictionary<string, LazynetSession> SessionDict { get; }
        public LazynetSessionManager()
        {
            this.SessionDict = new Dictionary<string, LazynetSession>();
        }

        public LazynetSession GetSession(string id)
        {
            if (this.SessionDict.ContainsKey(id))
            {
                return this.SessionDict[id];
            }
            return null;
        }
        
        public void AddSession(LazynetHandlerContext ctx)
        {
            string address = ctx.GetAddress();
            string id = EncryptionHelper.GetMD5Hash(address);
            this.SessionDict.Add(id, new LazynetSession()
            {
                Address = address,
                ID = id,
                ConnectDateTime = DateTime.Now,
                Context = ctx
            });
        }

        public void RemoveSession(string id)
        {
            if (this.SessionDict.ContainsKey(id))
            {
                this.SessionDict.Remove(id);
            }
        }

    }
}
