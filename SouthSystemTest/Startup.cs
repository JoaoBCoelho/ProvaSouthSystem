using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SouthSystemTest.Interfaces;
using SouthSystemTest.Services;
using System;
using System.IO;

namespace SouthSystemTest
{
    public class Startup
    {
        public static IServiceProvider Build()
        {
            IConfiguration configuration = GetConfiguration();
            ServiceCollection collection = ConfigureServices(configuration);

            IServiceProvider serviceProvider = collection.BuildServiceProvider();
            return serviceProvider;
        }

        private static ServiceCollection ConfigureServices(IConfiguration configuration)
        {
            var collection = new ServiceCollection();

            collection.AddScoped<IVendaService, VendaService>();
            collection.AddScoped<IMonitoramentoService, MonitoramentoService>();
            collection.AddScoped<IMapeadorService, MapeadorService>();
            collection.AddSingleton(configuration);

            return collection;
        }

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
               .SetBasePath(Path.Combine(AppContext.BaseDirectory))
               .AddJsonFile("appsettings.json", optional: true)
               .Build();
        }
    }
}
