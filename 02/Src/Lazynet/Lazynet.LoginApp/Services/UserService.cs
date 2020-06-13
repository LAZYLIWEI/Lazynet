/*
* ==============================================================================
*
* Filename:UserService
* Description: 
*
* Version: 1.0
* Created: 2020/5/24 21:24:12
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Core.Network;
using Lazynet.Core.Proto;
using Lazynet.Core.Service;
using Lazynet.Core.Util;
using Lazynet.DB.BLL;
using Lazynet.LoginApp.AppStart;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginApp.Services
{
    [LazynetServiceType(LazynetServiceType.Normal)]
    public class UserService : LazynetBaseService
    {
        protected UserBaseInfoBll UserBaseInfoBll { get; }
        public UserService(LazynetAppContext context)
            : base(context)
        {
            this.UserBaseInfoBll = new UserBaseInfoBll();
        }

        [LazynetServiceAction]
        public string SayHello(string msg)
        {
            this.Context.Logger.Log(msg);
            return "你好";
        }

        [LazynetServiceAction]
        public object Login(string username, string password)
        {
            bool result = this.UserBaseInfoBll.Login(username, password);
            return new
            {
                code = result ? 0 : 1,
                data = string.Empty,
                msg = result ? "登录成功" : "登录失败"
            };
        }

        [LazynetServiceAction]
        public void Register(string username, string password)
        {

        }

    }
}
