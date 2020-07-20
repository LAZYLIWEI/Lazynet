/*
* ==============================================================================
*
* Filename: DBProxy
* Description: 
*
* Version: 1.0
* Created: 2020/7/13 1:01:27
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.DB.BLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.DB
{
    public class DBProxy
    {
        public UserBaseInfoBll CreateUserBaseInfoBll()
        {
            UserBaseInfoBll userBaseInfoBll = new UserBaseInfoBll();
            return userBaseInfoBll;
        }
    }
}
