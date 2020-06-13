/*
* ==============================================================================
*
* Filename: LazynetSocket
* Description: 
*
* Version: 1.0
* Created: 2020/4/1 21:27:01
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
using System;
using System.Threading.Tasks;

namespace Lazynet.Network
{
    /// <summary>
    /// lazynet socket
    /// </summary>
    public class LazynetSocket :  ILazynetSocketContext
    {
        public MultithreadEventLoopGroup BossGroup { get; }
        public MultithreadEventLoopGroup WorkerGroup { get; }
        public Task<IChannel> BindResult { get; set; }
        public LazynetSocketConfig Config { get; }
        public ILazynetSocketEvent Event { get; set; }

        public LazynetSocket(LazynetSocketConfig config)
        {
            this.Config = config;
            this.BindResult = null;
            this.BossGroup = new MultithreadEventLoopGroup();
            this.WorkerGroup = new MultithreadEventLoopGroup();
        }

        public LazynetSocket SetEvent(ILazynetSocketEvent ev)
        {
            this.Event = ev;
            return this;
        }

        public void BindAsync() 
        {
            var bootstrap = new ServerBootstrap();
            bootstrap.Group(this.BossGroup, this.WorkerGroup)
                .Channel<TcpServerSocketChannel>()
                .Handler(new LoggingHandler(LogLevel.INFO));
            if (Config.Type == LazynetSocketType.TcpSocket)
            {
                bootstrap.ChildHandler(new LazynetTSChannelInitializer(this));
            }
            else if (Config.Type == LazynetSocketType.Websocket)
            {
                if (string.IsNullOrEmpty(this.Config.WSPath))
                {
                    throw new Exception("使用websocket时[path]参数必填");
                }
                bootstrap.ChildHandler(new LazynetWSChannelInitializer(this));
            }
            else
            {
                throw new Exception("没有其它类型的socket");
            }
            this.BindResult = bootstrap.BindAsync(this.Config.Port);
        }

        public void Close()
        {
            this.BindResult.Result?.CloseAsync();
            this.WorkerGroup.ShutdownGracefullyAsync();
            this.BossGroup.ShutdownGracefullyAsync();
        }

    }
}
