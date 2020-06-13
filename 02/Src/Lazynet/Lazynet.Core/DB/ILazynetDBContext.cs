using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.DB
{
    public interface ILazynetDBContext
    {
        SqlSugarClient CreateContext();
    }
}
