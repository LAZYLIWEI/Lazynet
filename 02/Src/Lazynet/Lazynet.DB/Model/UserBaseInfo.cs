using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.DB.Model
{
    /// <summary>
    /// 用户base
    /// </summary>
    public class UserBaseInfo
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreateDatetime { get; set; }
    }
}
