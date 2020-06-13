/*
* ==============================================================================
*
* Filename: LazynetSystemTrigger
* Description: 
*
* Version: 1.0
* Created: 2020/4/22 22:36:47
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Lazynet.Network;
using Lazynet.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazynet.Core
{
    internal class LazynetSystemTrigger : LazynetTriggerProvider
    {
        public LazynetSystemTrigger(ILazynetService serviceContext)
            : base(serviceContext)
        { }

        public void StartService(double serviceID)
        {
            var serivce = ServiceContext.Context.GetService(Convert.ToInt32(serviceID));
            if (serivce != null)
            {
                serivce?.Start();
            }
            else
            {
                ServiceContext.Context.Logger.Info(ServiceContext.ID.ToString(), serviceID + " no mapping");
            }
        }

        public void WriteAndFlush(LazynetChannelHandlerContext channelHandlerContext, string msg)
        {
            channelHandlerContext?.WriteAndFlushAsync(msg);
        }

        public void Write(LazynetChannelHandlerContext channelHandlerContext, string msg)
        {
            channelHandlerContext?.WriteAsync(msg);
        }

        public void SetSessionGroup(ILazynetSessionGroup sessionGroup)
        {
            ServiceContext.SetSessionGroup(sessionGroup);
        }

        public LazynetSession FindSession(string ID)
        {
            return ServiceContext.FindSession(ID);
        }

        public void AddSession(LazynetChannelHandlerContext channelHandlerContext)
        {
            ServiceContext.AddSession(channelHandlerContext);
        }

        public void RemoveSession(LazynetChannelHandlerContext channelHandlerContext)
        {
            ServiceContext.RemoveSession(channelHandlerContext);
        }

        public void ClearSession()
        {
            ServiceContext.ClearSession();
        }

        public void Log(string msg)
        {
            ServiceContext.Context.Logger.Info(ServiceContext.ID.ToString(), msg);
        }

        public void CreateSocket(double port, double heartbeat, double type)
        {
            ServiceContext.CreateSocket(new LazynetSocketConfig() {
                Heartbeat = Convert.ToInt32(heartbeat),
                Port = Convert.ToInt32(port),
                Type = (LazynetSocketType)type
            });
        }

        public void BindAsync(string activeEvent, string inactiveEvent, string readEvent, string exceptionEvent)
        {
            LazynetSocketEvent socketEvent = new LazynetSocketEvent()
            {
                ActiveEvent = activeEvent,
                ExceptionEvent = exceptionEvent,
                InactiveEvent = inactiveEvent,
                ReadEvent = readEvent
            };
            ServiceContext.BindAsync(socketEvent);
        }

        public void CloseSocket()
        {
            ServiceContext.CloseSocket();
        }

    }
}
