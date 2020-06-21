/*
* ==============================================================================
*
* Filename: SessionService
* Description: 
*
* Version: 1.0
* Created: 2020/6/22 0:08:45
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.AppCore;
using Lazynet.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.LoginApp.Services
{
    [LazynetServiceType(LazynetServiceType.Normal)]
    public class SessionService : LazynetBaseService
    {
        public SessionService(LazynetAppContext context)
             : base(context)
        {
        }


        [LazynetServiceAction]
        public void Connect()
        {

        }

        [LazynetServiceAction]
        public void Disconnect()
        {

        }

    }
}
