using System;
using System.Collections.Generic;
using System.Text;

namespace SouthSystemTest.DTO
{
    public class FileDTO
    {
        public FileDTO(string nomeArquivo)
        {
            this.NomeArquivo = nomeArquivo;
        }

        public String NomeArquivo { get; set; }
    }
}
