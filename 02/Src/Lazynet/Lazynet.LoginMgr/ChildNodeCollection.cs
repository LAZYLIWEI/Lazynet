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
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginMgr
{
    /// <summary>
    /// login app node
    /// </summary>
    public class ChildNodeCollection
    {
        public Dictionary<string, ChildNode> ChildNodeEntityDict { get; }
        public ChildNodeCollection()
        {
            this.ChildNodeEntityDict = new Dictionary<string, ChildNode>();
        }

        public ChildNode Find(string ID)
        {
            if (this.ChildNodeEntityDict.ContainsKey(ID))
            {
                return this.ChildNodeEntityDict[ID];
            }
            return default(ChildNode);
        }

        public void Add(string ID, ChildNode entity)
        {
            if (this.ChildNodeEntityDict.ContainsKey(ID))
            {
                this.ChildNodeEntityDict.Add(ID, entity);
            }
            else
            {
                this.ChildNodeEntityDict[ID] = entity;
            }
        }

        public void Remove(string ID)
        {
            if (this.ChildNodeEntityDict.ContainsKey(ID))
            {
                this.ChildNodeEntityDict.Remove(ID);
            }
        }

        public void Remove(ChildNode entity)
        {
            Remove(entity.Address);
        }

        public void Clear()
        {
            this.ChildNodeEntityDict.Clear();
        }

        public int Count()
        {
            return this.ChildNodeEntityDict.Count;
        }
    }
}
