using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Action
{
    public interface ILazynetAction
    {
        /// <summary>
        /// 调用action
        /// </summary>
        /// <param name="parameterArray"></param>
        /// <returns></returns>
        object[] Call(object[] parameterArray);
    }
}
