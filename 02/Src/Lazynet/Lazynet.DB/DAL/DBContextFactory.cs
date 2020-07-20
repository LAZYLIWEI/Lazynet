/*
* ==============================================================================
*
* Filename: DBContextFactory
* Description: 
*
* Version: 1.0
* Created: 2020/5/27 1:12:24
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.DB;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.DB.DAL
{
    public class DBContextFactory
    {
        public static string ConnectionString { get; set; }
        static DBContextFactory()
        {
            ConnectionString = " Data Source = 47.92.213.250; Initial Catalog = LazyGame; Persist Security Info=True;User ID = sa; Password=8g199696QQ";
        }

        public static SqlSugarClient CreateDbContext()
        {
            ILazynetDBContext db = new LazynetSqlSugarContext(new ConnectionConfig() {
                ConnectionString = ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.SystemTable,
                IsShardSameThread = true,
            });
            return db.CreateContext();
        }
    }
}
