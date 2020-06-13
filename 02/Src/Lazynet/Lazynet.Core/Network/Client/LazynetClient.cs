/*
* ==============================================================================
*
* Filename: LazynetClient
* Description: 
*
* Version: 1.0
* Created: 2020/5/4 0:46:51
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace Lazynet.Core.Network.Client
{
    public class LazynetClient : ILazynetClient
    {
        public IEventLoopGroup EventloopGroup { get; }
        public LazynetClientConfig Config { get; }
        public ILazynetSocketEvent SocketEvent { get; set; }


        /// <summary>
        /// socketEvent绑定之前必须赋值
        /// </summary>
        /// <param name="config"></param>
        public LazynetClient(LazynetClientConfig config)
            : this(config, null)
        {
        }

        public LazynetClient(LazynetClientConfig config, 
                                ILazynetSocketEvent socketEvent)
        {
            this.EventloopGroup = new MultithreadEventLoopGroup();
            this.Config = config;
            this.SocketEvent = socketEvent;
        }

     
        public ILazynetClient SetIP(string ip)
        {
            this.Config.IP = ip;
            return this;
        }

   
        public ILazynetClient SetPort(int port)
        {
            this.Config.Port = port;
            return this;
        }

        public ILazynetClient SetSocketEvent(ILazynetSocketEvent socketEvent)
        {
            if (socketEvent is null)
            {
                throw new Exception("socket event is null");
            }
            this.SocketEvent = socketEvent;
            return this;
        }


        private Bootstrap CreateBootstrap()
        {
            Bootstrap bootStrap = new Bootstrap();
            bootStrap.Group(this.EventloopGroup).Channel<TcpSocketChannel>().Handler(new LazynetClientInitalizer(this.Config, this.SocketEvent));
            return bootStrap;
        }

        public bool ConnectToHost()
        {
            Bootstrap bootStrap = CreateBootstrap();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(this.Config.IP), this.Config.Port);
            var channelFuture = bootStrap.ConnectAsync(endPoint);
            return channelFuture.Result.Open;
        }

        public bool TryConnectToHost(int time, int interval, Action<string> exception)
        {
            Bootstrap bootStrap = CreateBootstrap();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(this.Config.IP), this.Config.Port);
            bool isConnected = false;
            for (int i = 0; i < time && !isConnected; i++)
            {
                Thread.Sleep(interval);
                var channelFuture = bootStrap.ConnectAsync(endPoint);
                try
                {
                    isConnected = channelFuture.Result.Open;
                }
                catch (Exception ex)
                {
                    exception?.Invoke(ex.ToString());
                }
            }
            return isConnected;
        }


        public bool WaitConnectToHost(int interval, Action<string> exception)
        {
            Bootstrap bootStrap = CreateBootstrap();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(this.Config.IP), this.Config.Port);
            bool isConnected = false;
            do
            {
                Thread.Sleep(interval);
                var channelFuture = bootStrap.ConnectAsync(endPoint);
                try
                {
                    isConnected = channelFuture.Result.Open;
                }
                catch(Exception ex)
                {
                    exception?.Invoke(ex.ToString());
                }
            } while (!isConnected);
            return isConnected;
        }

    }
}
