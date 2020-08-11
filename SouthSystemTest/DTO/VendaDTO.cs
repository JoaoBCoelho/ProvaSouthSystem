using System.Collections.Generic;
using System.Linq;

namespace SouthSystemTest.DTO
{
    public class VendaDTO
    {
        public int Id { get; set; }
        public List<DadosVendaDTO> DadosVendas { get; set; }
        public VendedorDTO Vendedor { get; set; }
        public decimal ValorTotalVenda
        {
            get
            {
                return DadosVendas.Sum(s => s.ValorTotalVendaItem);
            }
        }

    }
}
