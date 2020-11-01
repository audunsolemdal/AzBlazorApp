using AzBlazorApp.Data.Services;
using AzBlazorApp.Data.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AzBlazorApp
{
    public abstract class AbstractStartup
    {
        protected void AddCustomServices(IServiceCollection services)
        {
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddScoped<IBlobStorageService, BlobStorageService>();
            // services.AddScoped<IQueueService, QueueService>();
        }
    }
}
