using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Serilog;

using System;
using System.IO;

namespace sabs_pos_backend_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Information()
                            .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}/logs/log-.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                            .CreateLogger();

            try
            {
                Log.Information($"Application start-up on {DateTime.Now:yyy-MM-dd HH:mm:ss.fff}");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
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
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSerilog();
    }
}
