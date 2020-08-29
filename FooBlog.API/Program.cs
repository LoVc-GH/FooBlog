using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog.Core;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;

namespace FooBlog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .UseSerilog((hostingContext, services, loggerConfiguration) => loggerConfiguration
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                    .Enrich.FromLogContext()
                    .WriteTo.Console());
    }
}
