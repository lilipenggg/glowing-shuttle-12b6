using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
	        var host = new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseStartup<Startup>()
            .Build();
	    
 	        host.Run();
        }
    }
}
