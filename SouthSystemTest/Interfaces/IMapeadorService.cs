using SouthSystemTest.DTO;
using System.Collections.Generic;

namespace SouthSystemTest.Interfaces
{
    interface IMapeadorService
    {
        EntradaDTO ConverterEntrada(List<string> conteudoArquivo, string nomeArquivo);
    }
}
