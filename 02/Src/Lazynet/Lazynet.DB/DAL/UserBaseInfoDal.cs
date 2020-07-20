/*
* ==============================================================================
*
* Filename: UserInfoBaseDal
* Description: 
*
* Version: 1.0
* Created: 2020/6/1 0:43:47
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.DB.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.DB.DAL
{
    public class UserBaseInfoDal : BaseDal<UserBaseInfo>
    {
        public UserBaseInfo Get(string userName)
        {
            var query = this.DB.Queryable<UserBaseInfo>();
            return query
                .Where(it => it.UserName == userName)
                .First();
        }

        public bool Insert(UserBaseInfo userBaseInfo)
        {
            var insertable = DB.Insertable(userBaseInfo);
            return insertable.ExecuteCommand() > 0;
        }


    }
}
