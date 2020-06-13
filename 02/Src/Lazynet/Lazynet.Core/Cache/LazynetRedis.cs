/*
* ==============================================================================
*
* Filename: LazynetRedis
* Description: 
*
* Version: 1.0
* Created: 2020/5/26 0:15:51
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Cache
{
    public class LazynetRedis : ILazynetCache<string>
    {
        public ConnectionMultiplexer Redis { get; }
        public IDatabase DB { get; }

        public LazynetRedis(string host)
        {
            this.Redis = ConnectionMultiplexer.Connect(host);
            this.DB = this.Redis.GetDatabase();
        }

        public LazynetRedis(string host, string password)
            : this(string.Format("{0},password={1}", host, password))
        {

        }

        public string Get(string key)
        {
            string value = this.DB.StringGet(key);
            return value;
        }

        public void Add(string key, string value)
        {
            if (this.DB.KeyExists(key))
            {
                this.Update(key, value);
            }
            else
            {
                this.Append(key, value);
            }
        }

        public void Append(string key, string value)
        {
            this.DB.StringAppend(key, value);
        }

        public void Remove(string key)
        {
            this.DB.KeyDelete(key);
        }

        public void Update(string key, string value)
        {
            this.DB.StringSet(key, value);
        }

    }
}
