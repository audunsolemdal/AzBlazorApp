using System;
using System.Threading.Tasks;
using AzBlazorApp.Data.Models;

namespace AzBlazorApp.Data.Services.Interfaces
{
    public interface IWeatherForecastService
    {
        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
    }
}
