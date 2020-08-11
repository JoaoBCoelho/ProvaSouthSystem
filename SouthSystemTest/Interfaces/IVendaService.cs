using SouthSystemTest.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthSystemTest.Interfaces
{
    public interface IVendaService
    {
        void ProcessarDadosVenda(EntradaDTO entrada);
    }
}
