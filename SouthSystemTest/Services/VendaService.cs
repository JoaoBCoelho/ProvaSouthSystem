using Microsoft.Extensions.Configuration;
using SouthSystemTest.DTO;
using SouthSystemTest.Interfaces;
using SouthSystemTest.Utils;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SouthSystemTest.Services
{
    public class VendaService : IVendaService
    {
        private readonly string _diretorioSaida;

        public VendaService(IConfiguration configuration)
        {
            _diretorioSaida = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + configuration.GetSection("DiretorioSaida").Value);
            FileUtils.ValidarDiretorio(_diretorioSaida);
        }

        public void ProcessarDadosVenda(EntradaDTO entrada)
        {
            SaidaDTO saidaDTO = GerarSaida(entrada);
            PersistirSaida(saidaDTO);
        }

        private void PersistirSaida(SaidaDTO saidaDTO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Id da venda mais cara: {saidaDTO.IdVendaMaisCara}");
            sb.AppendLine($"Nome do pior vendedor: {saidaDTO.NomePiorVendedor}");
            sb.AppendLine($"Quantidade de clientes: {saidaDTO.QtdClientes}");
            sb.AppendLine($"Quantidade de vendedores: {saidaDTO.QtdVendedores}");

            var texto = sb.ToString();

            FileUtils.GravarTexto(texto, Path.Combine(_diretorioSaida, saidaDTO.NomeArquivo));
        }

        //Assumi que não haverá mais de um vendedor com o mesmo valor de venda nem duas vendas com o mesmo valor máximo
        private SaidaDTO GerarSaida(EntradaDTO entrada)
        {
            SaidaDTO saidaDTO = new SaidaDTO(entrada.NomeArquivo);

            saidaDTO.IdVendaMaisCara = ObterVendaMaisCara(entrada).Id;
            saidaDTO.NomePiorVendedor = ObterPiorVendedor(entrada).Nome;
            saidaDTO.QtdClientes = entrada.Clientes.GroupBy(g => g.CNPJ).Count();
            saidaDTO.QtdVendedores = entrada.Vendedores.GroupBy(g => g.CPF).Count();

            return saidaDTO;
        }

        private VendedorDTO ObterPiorVendedor(EntradaDTO entrada)
        {
            var vendasPorVendedor = entrada.Vendas.GroupBy(g => g.Vendedor);
            var vendasAgrupadas = vendasPorVendedor.Select(s => new { Vendedor = s.Key, TotalVendas = s.Sum(s => s.ValorTotalVenda) });
            var vendedor = vendasAgrupadas.FirstOrDefault(f => f.TotalVendas == vendasAgrupadas.Min(m => m.TotalVendas)).Vendedor;

            return vendedor;
        }

        private VendaDTO ObterVendaMaisCara(EntradaDTO entrada)
            => entrada.Vendas.FirstOrDefault(f => f.ValorTotalVenda == entrada.Vendas.Max(m => m.ValorTotalVenda));

    }
}
