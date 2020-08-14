using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotAPIGw
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                        .AddJsonFile("ocelot.json")
                        .AddEnvironmentVariables();
                })
                .ConfigureServices(s => {
                    var identityUrl = "http://identityserver";
                    var authenticationProviderKey = "IdentityApiKey";
                    //…
                    s.AddAuthentication()
                        .AddJwtBearer(authenticationProviderKey, x =>
                        {
                            x.Authority = identityUrl;
                            x.RequireHttpsMetadata = false;
                            x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                            {

                                ValidateIssuer = false,
                                ValidAudiences = new[] { "weather", "basket", "locations", "marketing", "mobileshoppingagg", "webshoppingagg" }
                            };
                        });
                    s.AddOcelot();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConsole();
                    logging.AddDebug();
                    //add your logging
                })
                .UseIISIntegration()
                .Configure(app =>
                {
                    IdentityModelEventSource.ShowPII = true;

                    app.UseOcelot().Wait();
                })
                .Build()
                .Run();
        }
    }
}
