using Microsoft.Extensions.DependencyInjection;
using SouthSystemTest.Interfaces;

namespace SouthSystemTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = Startup.Build();
            var monitoramentoService = serviceProvider.GetService<IMonitoramentoService>();
            monitoramentoService.MonitorarArquivos();
        }
    }
}
