﻿using Lazynet.LoginApp.AppStart;
using System;

namespace Lazynet.LoginApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LazynetAppManager
                .GetInstance()
                .UseStartup<Startup>()
                .Builder()
                .Start();
            Console.ReadKey();
        }
    }
}