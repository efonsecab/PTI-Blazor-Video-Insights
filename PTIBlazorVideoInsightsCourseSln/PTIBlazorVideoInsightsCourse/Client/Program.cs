using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Microsoft.JSInterop;
using PTIBlazorVideoInsightsCourse.Shared.CustomLogging;

namespace PTIBlazorVideoInsightsCourse.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddLocalization();
            var host = builder.Build();
            var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
            var result = await jsInterop.InvokeAsync<string>("blazorCulture.get");
            if (result != null)
            {
                var culture = new CultureInfo(result);
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
            else
            {
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentUICulture =
                    CultureInfo.GetCultureInfo("en");
            }
            builder.Services.AddLogging(configure =>
            {
                CustomLoggerConfiguration customLoggerConfiguration = 
                builder.Configuration.GetSection("CustomLoggerConfiguration").Get<CustomLoggerConfiguration>();
                configure.AddProvider(new CustomLoggerProvider(
                    customLoggerConfiguration
                    ));
            });

            await builder.Build().RunAsync();
        }
    }
}
