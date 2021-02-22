using ChessMeters.Core.Extensions;
using ChessMeters.Core.Jobs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Quartz;
using System;

namespace ChessMeters.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var mySqlConnectionString = configuration.GetConnectionString("ChessMeters");
                    config.AddDatabaseConfiguration(options => options.UseMySql(mySqlConnectionString,
                        ServerVersion.AutoDetect(mySqlConnectionString)));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.ListenAnyIP(6000, listenOptions =>
                        {
                            var x = configuration["IdentityServer__Key__Password"];
                            listenOptions.UseHttps("certificate.pfx", Environment.GetEnvironmentVariable("IdentityServer__Key__Password"));
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                }).ConfigureServices((hostContext, services) =>
                {
                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionScopedJobFactory();
                        q.AddJob<ReportGeneratorJob>(opts =>
                        {
                            opts.WithIdentity(new JobKey(typeof(ReportGeneratorJob).Name)).StoreDurably();
                        });
                    });

                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                }).UseNLog();
        }
    }
}
