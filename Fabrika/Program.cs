using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Fabrika
{
    public class Program
    {
		/// <summary>
		///
		/// </summary>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

		/// <summary>
		///
		/// </summary>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
				.UseUrls("http://localhost:5555/") 
                .Build();
    }
}
