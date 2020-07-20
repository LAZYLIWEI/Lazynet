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
        public UserBaseInfoDal UserBaseInfoDal { get; }
        public UserBaseInfoBll()
        {
            this.UserBaseInfoDal = new UserBaseInfoDal();
        }

        public bool Login(string userName, string password)
        {
            var usserBaseInfo = this.UserBaseInfoDal.Get(userName);
            if (usserBaseInfo == null)
            {
                return false;
            }
            if (usserBaseInfo.Password != EncryptionHelper.GetMD5Hash(password))
            {
                return false;
            }
            return true;
        }

        public bool Register(string userName, string password)
        {
            var usserBaseInfo = this.UserBaseInfoDal.Get(userName);
            if (usserBaseInfo != null)
            {
                return false;
            }
            string encryptPassword = EncryptionHelper.GetMD5Hash(password);
            string id = SnowflakeHelper.Instance().GetString();
            usserBaseInfo = new Model.UserBaseInfo()
            {
                ID = id,
                CreateDatetime = DateTime.Now,
                Password = encryptPassword,
                UserName = userName
            };
            return this.UserBaseInfoDal.Insert(usserBaseInfo);
        }
    }
}
