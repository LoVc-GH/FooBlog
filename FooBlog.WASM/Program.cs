using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StardustDL.RazorComponents.Markdown;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Sotsera.Blazor.Toaster.Core.Models;

namespace FooBlog.WASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");


            builder.Services.AddHttpClient("authenticated", options => options.BaseAddress = new Uri("https://localhost:44324/odata/"))
                .AddHttpMessageHandler(sp =>
                {
                    var handler = sp.GetService<AuthorizationMessageHandler>()
                         .ConfigureHandler(
                             authorizedUrls: new[] { "https://localhost:44324/" },
                             scopes: new[] { "FooBlog_api" });
                    return handler;
                });

            builder.Services.AddHttpClient("anonymous", options => options.BaseAddress = new Uri("https://localhost:44324/odata/"));

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("oidc", options.ProviderOptions);
                options.UserOptions.RoleClaim = "role";
            });

            builder.Services.AddSingleton<IMarkdownComponentService, MarkdownComponentService>();
            builder.Services.AddToaster(config =>
            {
                //example customizations
                config.PositionClass = Defaults.Classes.Position.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = false;                
            });

            await builder.Build().RunAsync();
        }
    }
}
