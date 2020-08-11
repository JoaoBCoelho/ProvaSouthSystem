using System;
using System.Collections.Generic;
using System.Text;

namespace SouthSystemTest.DTO
{
    public class SaidaDTO : FileDTO
    {
        public SaidaDTO(string nomeArquivo) : base(nomeArquivo) { }

        public int QtdClientes { get; set; }
        public int QtdVendedores { get; set; }
        public int IdVendaMaisCara { get; set; }
        public String NomePiorVendedor { get; set; }
    }
}
