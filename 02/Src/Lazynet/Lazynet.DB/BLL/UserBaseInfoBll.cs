/*
* ==============================================================================
*
* Filename: UserInfoBll
* Description: 
*
* Version: 1.0
* Created: 2020/6/1 0:44:36
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Util;
using Lazynet.DB.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.DB.BLL
{
    public class UserBaseInfoBll
    {
        public bool Login(string userName, string password)
        {
            var userBaseInfoDal = new UserBaseInfoDal();
            var usserBaseInfo = userBaseInfoDal.Get(userName);
            if (usserBaseInfo == null
                || usserBaseInfo.Password != EncryptionHelper.GetMD5Hash(password))
            {
                return false;
            }
            return true;
        }
    }
}
