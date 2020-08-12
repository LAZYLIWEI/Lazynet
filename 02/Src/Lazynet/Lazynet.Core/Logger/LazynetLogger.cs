/*
* ==============================================================================
*
* Filename: LazynetLogger
* Description: 
*
* Version: 1.0
* Created: 2020/5/3 22:27:24
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lazynet.Core.Logger
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public class LazynetLogger : ILazynetLogger
    {
        private ILog logger;
        public LazynetLogger(string configFilename)
        {
            var repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(repository, new FileInfo(configFilename));
            logger = LogManager.GetLogger(repository.Name, "InfoLogger");
        }


        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="content"></param>
        public void Info(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }
            logger.Info(content);
        }


        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="content"></param>
        public void Debug(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }
            logger.Debug(content);
        }


        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="content"></param>
        public void Warn(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }
            logger.Warn(content);
        }


        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="content"></param>
        public void Error(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }
            logger.Error(content);
        }


    }
}
