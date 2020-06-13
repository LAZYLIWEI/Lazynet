using Lazynet.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core
{
    /// <summary>
    /// session group
    /// </summary>
    public interface ILazynetSessionGroup
    {
        LazynetSession Find(string ID);
        LazynetSession Find(LazynetChannelHandlerContext ctx);
        void Add(LazynetSession session);
        void Remove(LazynetSession session);
        void Clear();
    }
}
