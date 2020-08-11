using SouthSystemTest.DTO;
using SouthSystemTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SouthSystemTest.Services
{
    public class MapeadorService : IMapeadorService
    {
        public EntradaDTO ConverterEntrada(List<string> conteudoArquivo, string nomeArquivo)
        {
            var entradaDTO = new EntradaDTO(nomeArquivo);

            var linhasVendedor = ObterLinhasPorId(conteudoArquivo, "001");
            entradaDTO.Vendedores = ObterVendedores(linhasVendedor);

            var linhasCliente = ObterLinhasPorId(conteudoArquivo, "002");
            entradaDTO.Clientes = ObterClientes(linhasCliente);

            var linhasVendas = ObterLinhasPorId(conteudoArquivo, "003");
            entradaDTO.Vendas = ObterVendas(linhasVendas, entradaDTO.Vendedores);

            return entradaDTO;
        }

        private List<VendedorDTO> ObterVendedores(List<string> vendedores)
        {
            var ret = new List<VendedorDTO>();

            foreach (var linha in vendedores)
            {
                var dados = linha.Split('ç');

                var vendedor = new VendedorDTO();
                vendedor.CPF = dados[1];
                vendedor.Nome = dados[2];
                vendedor.Salario = Convert.ToDecimal(dados[3]);

                ret.Add(vendedor);
            }

            return ret;
        }

        private List<ClienteDTO> ObterClientes(List<string> clientes)
        {
            var ret = new List<ClienteDTO>();

            foreach (var linha in clientes)
            {
                var dados = linha.Split('ç');

                var cliente = new ClienteDTO();
                cliente.CNPJ = dados[1].PadLeft(14, '0');
                cliente.Nome = dados[2];
                cliente.AreaNegocio = dados[3];

                ret.Add(cliente);
            }

            return ret;
        }

        private List<VendaDTO> ObterVendas(List<string> vendas, List<VendedorDTO> vendedores)
        {
            var ret = new List<VendaDTO>();

            foreach (var linha in vendas)
            {
                var dados = linha.Split('ç');

                var venda = new VendaDTO();
                venda.Id = Convert.ToInt32(dados[1]);
                venda.DadosVendas = ObterDadosVenda(dados[2]);
                venda.Vendedor = vendedores.FirstOrDefault(f => f.Nome == dados[3].Trim());

                ret.Add(venda);
            }

            return ret;
        }

        private List<DadosVendaDTO> ObterDadosVenda(String dadosVenda)
        {
            var ret = new List<DadosVendaDTO>();
            var dados = dadosVenda.Replace("[", String.Empty)
                                  .Replace("]", String.Empty)
                                  .Split(',');

            foreach (var dado in dados)
            {
                var registro = dado.Split('-');

                var vendas = new DadosVendaDTO();
                vendas.Id = Convert.ToInt32(registro[0]);
                vendas.Quantidade = Convert.ToInt32(registro[1]);
                vendas.Preco = Convert.ToDecimal(registro[2]);

                ret.Add(vendas);
            }

            return ret;
        }

        private List<string> ObterLinhasPorId(List<string> conteudoArquivo, string id)
            => conteudoArquivo.Where(w => w.StartsWith(id))
                               .Select(s => s)
                               .ToList();

    }
}
