/*
* ==============================================================================
*
* Filename: LazynetSqlSugarContext
* Description: 
*
* Version: 1.0
* Created: 2020/5/27 0:44:38
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.DB
{
    public class LazynetSqlSugarContext : ILazynetDBContext
    {
        /// <summary>
        /// 主数据库连接配置
        /// </summary>
        public ConnectionConfig ConnectionConfig { get; }
        public LazynetSqlSugarContext(ConnectionConfig connectionConfig)
        {
            this.ConnectionConfig = connectionConfig;
        }

        public SqlSugarClient CreateContext()
        {
            SqlSugarClient client = new SqlSugarClient(this.ConnectionConfig);
            return client;
        }
    }
}
