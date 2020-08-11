namespace SouthSystemTest.DTO
{
    public class DadosVendaDTO
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public decimal ValorTotalVendaItem
        {
            get
            {
                return Quantidade * Preco;
            }
        }
    }
}
