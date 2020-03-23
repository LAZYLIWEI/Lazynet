using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Net;

namespace Lazynet.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IEventLoopGroup eventloopGroup = new MultithreadEventLoopGroup();
            try
            {
                Bootstrap bootStrap = new Bootstrap();
                bootStrap.Group(eventloopGroup).Channel<TcpSocketChannel>().Handler(new MyClientInitalizer());
                var channelFuture = bootStrap.ConnectAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 30000));
                Console.ReadKey();
                channelFuture.Result.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                eventloopGroup.ShutdownGracefullyAsync();
            }

        }
    }
}
