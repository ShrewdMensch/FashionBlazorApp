using Application.Repository;
using FashionAppBlazor.Client.Services.Contract;
using FashionAppBlazor.Client.Services.Implementation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FashionAppBlazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IAccessoryService, AccessoryService>();
            builder.Services.AddScoped<IMeasurementHeaderService, MeasurementHeaderService>();
            builder.Services.AddScoped<IStandardSizeService, StandardSizeService>();
            builder.Services.AddScoped<IOperatingExpenseService, OperatingExpenseService>();
            builder.Services.AddScoped<IIncurredExpenseService, IncurredExpenseService>();
            builder.Services.AddScoped<ITypeOfClothService, TypeOfClothService>();
            builder.Services.AddAutoMapper(typeof(IRepository));

            await builder.Build().RunAsync();
        }
    }
}
