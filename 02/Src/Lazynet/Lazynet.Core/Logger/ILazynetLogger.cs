using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Logger
{
    public interface ILazynetLogger
    {
        void Info(string content);
        void Debug(string content);
        void Warn(string content);
        void Error(string content);
    }
}
