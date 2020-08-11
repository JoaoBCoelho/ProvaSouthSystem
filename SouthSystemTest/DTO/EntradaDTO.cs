using System.Collections.Generic;

namespace SouthSystemTest.DTO
{
    public class EntradaDTO : FileDTO
    {
        public EntradaDTO(string nomeArquivo) : base(nomeArquivo) { }

        public List<ClienteDTO> Clientes { get; set; }
        public List<VendedorDTO> Vendedores { get; set; }
        public List<VendaDTO> Vendas { get; set; }
    }
}
