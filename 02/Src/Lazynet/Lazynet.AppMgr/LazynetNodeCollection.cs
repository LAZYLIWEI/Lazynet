/*
* ==============================================================================
*
* Filename: MemberCollection
* Description: 
*
* Version: 1.0
* Created: 2020/5/22 23:33:20
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
using System.Linq;
using System.Text;

namespace Lazynet.AppMgr
{
    /// <summary>
    /// node
    /// </summary>
    public class LazynetNodeCollection
    {
        public List<LazynetNode> Nodes { get; }
        public LazynetNodeCollection()
        {
            this.Nodes = new List<LazynetNode>();
        }


        public LazynetNode GetByID(string ID)
        {
            foreach (var item in Nodes)
            {
                if (item.ID == ID)
                {
                    return item;
                }
            }
            return null;
        }

        public LazynetNode GetByName(string name)
        {
            foreach (var item in Nodes)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }

            return null;
        }


        public void Add(string name, LazynetHandlerContext ctx)
        {
            var findNode = this.Nodes
                .Where(it => it.Name == name)
                .FirstOrDefault();
            if (findNode == null)
            {
                this.Nodes.Add(new LazynetNode()
                {
                    Address = ctx.GetAddress(),
                    ConnectDateTime = DateTime.Now,
                    Context = ctx,
                    ID = EncryptionHelper.GetMD5Hash(ctx.GetAddress()),
                    Name = name
                });
            }
        }


        public void Add(LazynetNode node)
        {
            var findNode = this.Nodes
                .Where(it => it.Name == node.Name)
                .FirstOrDefault();
            if (findNode == null)
            {
                this.Nodes.Add(node);
            }
        }

        public void RemoveByID(string ID)
        {
            var findNode = this.Nodes
                .Where(it => it.ID == ID)
                .FirstOrDefault();
            if (findNode != null)
            {
                this.Nodes.Remove(findNode);
            }
        }

        public void RemoveByName(string name)
        {
            var findNode = this.Nodes
                .Where(it => it.Name == name)
                .FirstOrDefault();
            if (findNode != null)
            {
                this.Nodes.Remove(findNode);
            }
        }


        public void Clear()
        {
            this.Nodes.Clear();
        }

        public int Count()
        {
            return this.Nodes.Count;
        }
    }
}
