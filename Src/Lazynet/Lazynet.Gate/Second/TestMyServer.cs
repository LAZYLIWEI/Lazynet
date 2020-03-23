using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Lazynet.Gate.Second;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lazynet.Gate
{
    public class TestMyServer
    {
        public static async Task RunAsync()
        {
            IEventLoopGroup bossGroup = new MultithreadEventLoopGroup();
            IEventLoopGroup workerGroup = new MultithreadEventLoopGroup();
            try
            {
                // 服务器引导程序
                var bootstrap = new ServerBootstrap();
                bootstrap.Group(bossGroup, workerGroup);
                bootstrap.Channel<TcpServerSocketChannel>();
                bootstrap.ChildHandler(new TestChannelInitializerHandler());
                IChannel boundChannel = await bootstrap.BindAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 30000));
                Console.ReadKey();
                await boundChannel.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                await bossGroup.ShutdownGracefullyAsync();
                await workerGroup.ShutdownGracefullyAsync();
            }
        }
    }
}
