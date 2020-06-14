/*
* ==============================================================================
*
* Filename: LazynetInteriorServerContext
* Description: 
*
* Version: 1.0
* Created: 2020/6/13 12:39:41
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Network.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginMgr.AppStart
{
    public class LazynetInteriorServerContext
    {
        public ILazynetServer Handler { get; set; }
        public Dictionary<string, LazynetSession> SessionDict { get; set; }

        /// <summary>
        /// 将返回的结果返回给前端
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="message"></param>
        public void Response(string ID, string message)
        {
            // 校验
            if (!this.SessionDict.ContainsKey(ID))
            {
                //Context.Logger.Log(" dont's have the ID: " + ID);
                return;
            }
            var session = this.SessionDict[ID];
            session.Context.WriteAndFlushAsync(message);
        }

    }
}
