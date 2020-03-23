using DotNetty.Codecs.Http;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lazynet.Gate.First
{
    public class TestHttpServer
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
                bootstrap.ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast(new HttpServerCodec());
                    pipeline.AddLast(new TestHttpServerHandler());
                }));

                IChannel boundChannel = await bootstrap.BindAsync(3000);
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
