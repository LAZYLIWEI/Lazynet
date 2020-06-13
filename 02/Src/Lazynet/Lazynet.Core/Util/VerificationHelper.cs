/*
* ==============================================================================
*
* Filename: VerificationHelper
* Description: 
*
* Version: 1.0
* Created: 2020/5/4 14:10:35
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core.Util
{
    public class VerificationHelper
    {
        public static bool Range(int value, int min, int max)
        {
            return value >= min && value <= max;
        }

    }
}
