using System.IO;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApiWithBackgroundWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>();
             

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //         CreateCustomWebBuilder(args)
        //        .UseStartup<Startup>();

        ///// <summary>
        /////   Initializes a new instance of the <see cref="WebHostBuilder"/> class with pre-configured defaults.
        ///// </summary>
        ///// <remarks>
        /////   The following defaults are applied to the returned <see cref="WebHostBuilder"/>:
        /////     use Kestrel as the web server,
        /////     set the <see cref="IHostingEnvironment.ContentRootPath"/> to the result of <see cref="Directory.GetCurrentDirectory()"/>,
        /////     load <see cref="IConfiguration"/> from 'appsettings.json' and 'appsettings.[<see cref="IHostingEnvironment.EnvironmentName"/>].json',
        /////     load <see cref="IConfiguration"/> from User Secrets when <see cref="IHostingEnvironment.EnvironmentName"/> is 'Local' using the entry assembly,
        /////     load <see cref="IConfiguration"/> from environment variables,
        /////     load <see cref="IConfiguration"/> from supplied command line args,
        /////     configures the <see cref="ILoggerFactory"/> to log to the console and debug output,
        /////     enables IIS integration,
        /////     enables the ability for frameworks to bind their options to their default configuration sections,
        /////     and adds the developer exception page when <see cref="IHostingEnvironment.EnvironmentName"/> is 'Local' or 'Dev'
        ///// </remarks>
        ///// <param name="args">The command line args.</param>
        ///// <returns>The initialized <see cref="IWebHostBuilder"/>.</returns>
        //public static IWebHostBuilder CreateCustomWebBuilder(string[] args)
        //{
        //    var builder = new WebHostBuilder();

        //    if (string.IsNullOrEmpty(builder.GetSetting(WebHostDefaults.ContentRootKey)))
        //    {
        //        builder.UseContentRoot(Directory.GetCurrentDirectory());
        //    }
        //    if (args != null)
        //    {
        //        builder.UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build());
        //    }
        //    builder.UseKestrel()
        //    .ConfigureAppConfiguration((hostingContext, config) =>
        //    {
        //        var env = hostingContext.HostingEnvironment;
        //        config
        //            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
        //            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //            .AddJsonFile($"appsettings.{env.EnvironmentName.ToLower()}.json", optional: true, reloadOnChange: true)
        //            .AddJsonFile($"appsettings.{env.EnvironmentName.ToLower()}.secrets.json", optional: true, reloadOnChange: true)
        //            .AddEnvironmentVariables();

        //        if (env.IsDevelopment())
        //        {
        //            var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
        //            if (appAssembly != null)
        //            {
        //                config.AddUserSecrets(appAssembly, optional: true);
        //            }
        //        }
        //        if (args != null)
        //        {
        //            config.AddCommandLine(args);
        //        }
        //    })
        //        .UseIISIntegration()
        //        .UseDefaultServiceProvider((context, options) =>
        //        {
        //            options.ValidateScopes = context.HostingEnvironment.IsDevelopment() || context.HostingEnvironment.IsEnvironment("dev");
        //        });

        //    return builder;
        //}
    }
}
