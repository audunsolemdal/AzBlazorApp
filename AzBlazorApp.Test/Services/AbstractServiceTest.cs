using System;
using System.IO;
using AzBlazorApp.Data;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AzBlazorApp.Test.Services
{
    public abstract class AbstractServiceTest<T>
    {
        private static readonly string AppConfigurationFile = "appsettings.json";
        private readonly IConfiguration configuration;
        protected string ConnString { get; }

        public AbstractServiceTest()
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppConfigurationFile))
                .AddUserSecrets(Constants.UserSecretsId)
                .Build();
            string connString = configuration.GetConnectionString("AzuriteSAKey");
            ConnString = connString;
        }

        public static ILogger<TS> GetLoggerFake<TS>()
            where TS : class
            => new Fake<ILogger<TS>>().FakedObject;

        protected abstract T NewService();

        protected void RunOnService(Action<T> action)
        {
                var service = NewService();
                action.Invoke(service);
        }
    }
}
