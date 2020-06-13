/*
* ==============================================================================
*
* Filename: LazynetServer
* Description: 
*
* Version: 1.0
* Created: 2020/5/3 23:43:23
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Lazynet.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lazynet.Core.Network.Server
{
    public class LazynetServer : ILazynetServer
    {
        public MultithreadEventLoopGroup BossGroup { get; }
        public MultithreadEventLoopGroup WorkerGroup { get; }
        public IChannel BindResult { get; set; }
        public LazynetServerConfig Config { get; set; }
        public ILazynetSocketEvent SocketEvent { get; set; }

        /// <summary>
        /// socketEvent绑定之前必须赋值
        /// </summary>
        /// <param name="config"></param>
        public LazynetServer(LazynetServerConfig config)
            : this (null, config)
        {
        }

        public LazynetServer(ILazynetSocketEvent socketEvent, LazynetServerConfig config)
        {
            this.BossGroup = new MultithreadEventLoopGroup();
            this.WorkerGroup = new MultithreadEventLoopGroup();
            this.Config = config;
            this.SocketEvent = socketEvent;
        }

        public int GetPort()
        {
            return this.Config.Port;
        }
     
        public ILazynetServer SetPort(int port)
        {
            this.Config.Port = port;
            return this;
        }

        public ILazynetServer SetSocketEvent(ILazynetSocketEvent socketEvent)
        {
            if (socketEvent is null)
            {
                throw new Exception("SocketEvent为null");
            }
            this.SocketEvent = socketEvent;
            return this;
        }

        public void Bind()
        {
            if (!this.Config.IsValid(out string errorMsg))
            {
                throw new Exception(errorMsg);
            }
            if (this.SocketEvent is null)
            {
                throw new Exception("SocketEvent未初始化");
            }

            var bootstrap = new ServerBootstrap();
            bootstrap.Group(this.BossGroup, this.WorkerGroup)
                .Channel<TcpServerSocketChannel>()
                .Handler(new LoggingHandler(LogLevel.INFO))
                .ChildHandler(new LazynetServerInitializer(this.Config, this.SocketEvent));
            this.BindResult = bootstrap.BindAsync(this.Config.Port).Result;
        }

        public void Close()
        {
            this.BindResult?.CloseAsync();
            this.WorkerGroup.ShutdownGracefullyAsync();
            this.BossGroup.ShutdownGracefullyAsync();
        }

       
    }
}
