using Microsoft.Extensions.DependencyInjection;
using AzBlazorApp.Data.Services;
using AzBlazorApp.Data.Services.Interfaces;

namespace AzBlazorApp
{
    public abstract class AbstractStartup
    {
        protected void AddCustomServices(IServiceCollection services)
        {
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddScoped<IBlobStorageService, BlobStorageService>();
            //services.AddScoped<IQueueService, QueueService>();
        }
    }
}
