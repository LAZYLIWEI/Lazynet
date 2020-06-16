/*
* ==============================================================================
*
* Filename: LazynetExternalServerContext
* Description: 
*
* Version: 1.0
* Created: 2020/6/13 11:47:39
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Network.Server;
using Lazynet.Core.Proto;
using Lazynet.Core.Service;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.AppMgrCore
{
    public class LazynetExternalServerContext
    {
        public int GlobaIndex { get; set; }
        public Dictionary<string, ILazynetService> ServiceDict { get; set; }
        public LazynetChildNodeCollection ChildNodeCollection { get; set; }
        public ILazynetServer Handler { get; set; }

        /// <summary>
        /// 查找一个节点,将消息分发给那个节点
        /// </summary>
        /// <param name="message"></param>
        public void Dispatch(LazynetFromMessage message)
        {
            if (this.ChildNodeCollection.Count() <= 0)
            {
                return;
            }
            LazynetChildNode childNode = null;
            int i = 0;
            foreach (var item in this.ChildNodeCollection.ChildNodeEntityDict)
            {
                int index = GlobaIndex % this.ChildNodeCollection.Count();
                if (i == index)
                {
                    childNode = item.Value;
                    break;
                }
                i++;
            }
            if (GlobaIndex >= int.MaxValue)
            {
                GlobaIndex = 0;
            }
            else
            {
                GlobaIndex++;
            }

            if (childNode == null)
            {
                return;
            }

            childNode.Context.WriteAndFlushAsync(SerializeHelper.Serialize(message));
        }

    }
}
