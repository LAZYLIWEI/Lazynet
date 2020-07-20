/*
* ==============================================================================
*
* Filename:ILazynetAppContext
* Description: 
*
* Version: 1.0
* Created: 2020/5/30 1:30:55
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/

using Lazynet.Core.Logger;

namespace Lazynet.AppCore
{
    public interface ILazynetAppContext
    {
        void Log(LazynetLogLevel level, string content);
    }
}
