using System;
using System.IO;
using System.Threading.Tasks;
using SouthSystemTest.Interfaces;
using Microsoft.Extensions.Configuration;
using SouthSystemTest.Utils;
using System.Linq;

namespace SouthSystemTest.Services
{
    class MonitoramentoService : IMonitoramentoService
    {
        private readonly FileSystemWatcher Monitoramento;
        private readonly IVendaService VendaService;
        private readonly IMapeadorService MapeadorService;

        public MonitoramentoService(IVendaService vendaService, IMapeadorService mapeadorService, IConfiguration configuration)
        {
            this.VendaService = vendaService;
            this.MapeadorService = mapeadorService;

            var filtro = configuration.GetSection("Filtro").Value;
            var diretorioEntrada = configuration.GetSection("DiretorioEntrada").Value;
            var caminhoEntradaCompleto = ObterDiretorioEntrada(diretorioEntrada);

            this.Monitoramento = CriarMonitoramento(filtro, caminhoEntradaCompleto);

            Console.WriteLine($"Monitorando arquivos {Monitoramento.Filter} em: {Monitoramento.Path}");
        }

        public async Task MonitorarArquivos()
        {
            Monitoramento.WaitForChanged(WatcherChangeTypes.All);
            await MonitorarArquivos();
        }

        private string ObterDiretorioEntrada(string caminhoPastaEntrada)
        {
            var CaminhoArquivoEntrada = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + caminhoPastaEntrada);
            FileUtils.ValidarDiretorio(CaminhoArquivoEntrada);

            return CaminhoArquivoEntrada;
        }

        private FileSystemWatcher CriarMonitoramento(string filtro, string caminhoArquivoEntrada)
        {
            var monitoramento = new FileSystemWatcher(caminhoArquivoEntrada, filtro)
            {
                EnableRaisingEvents = true,
            };

            monitoramento.Changed += ProcessaArquivo;
            monitoramento.Created += ProcessaArquivo;

            return monitoramento;
        }

        private void ProcessaArquivo(object sender, FileSystemEventArgs file)
        {
            try
            {
                Console.WriteLine($"Processa: {file.Name}- Início");
                var conteudoArquivo = File.ReadAllLines(file.FullPath);
                var entradaConvertida = MapeadorService.ConverterEntrada(conteudoArquivo.ToList(), file.Name);
                VendaService.ProcessarDadosVenda(entradaConvertida);
                Console.WriteLine($"Processa: {file.Name}- Fim");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Processa: { file.Name}- Erro\nMensagem de Exceção: {e.Message}");
            }
        }
    }
}
