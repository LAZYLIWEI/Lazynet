/*
* ==============================================================================
*
* Filename: LazynetDefaultSessionGroup
* Description: 
*
* Version: 1.0
* Created: 2020/4/18 17:39:01
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core
{
    public class LazynetDefaultSessionGroup : ILazynetSessionGroup
    {
        public Dictionary<string, LazynetSession> SessionGroup { get; }
        public LazynetDefaultSessionGroup()
        {
            this.SessionGroup = new Dictionary<string, LazynetSession>();
        }

        public LazynetSession Find(string ID)
        {
            if (this.SessionGroup.ContainsKey(ID))
            {
                return this.SessionGroup[ID];
            }
            else
            {
                return null;
            }
        }

        public void Add(LazynetSession session)
        {
            if (this.SessionGroup.ContainsKey(session.ID))
            {
                this.SessionGroup.Remove(session.ID);
            }
            this.SessionGroup.Add(session.ID, session);
        }

        public void Clear()
        {
            this.SessionGroup.Clear();
        }

        public void Remove(LazynetSession session)
        {
            if (this.SessionGroup.ContainsKey(session.ID))
            {
                this.SessionGroup.Remove(session.ID);
            }
        }
    }
}
