using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using System;
using System.IO;

namespace sabs_pos_test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    if (!string.IsNullOrEmpty(env))
                        builder.AddJsonFile($"appsettings.{env}.json", optional: true);
                    builder.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder.UseKestrel();
                    webHostBuilder.UseIISIntegration();
                    webHostBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
                    webHostBuilder.UseStartup<Startup>();
                })
                .UseContentRoot(Directory.GetCurrentDirectory());
    }
}
