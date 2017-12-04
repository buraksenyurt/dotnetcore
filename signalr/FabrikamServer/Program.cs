using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FabrikamServer
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()            
            .Build()
            .Run();
        }
    }
}