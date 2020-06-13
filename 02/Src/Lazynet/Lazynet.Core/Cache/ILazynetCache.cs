using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Cache
{
    public interface ILazynetCache<T>
    {
        T Get(string key);
        void Append(string key, T value);
        void Add(string key, T value);
        void Remove(string key);
        void Update(string key, T value);
    }
}
