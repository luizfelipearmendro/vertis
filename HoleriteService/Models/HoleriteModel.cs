namespace HoleriteService.Models
{
    public class HoleriteModel
    {
        public int EmployeeId { get; set; } // ID do funcionário
        public string Name { get; set; } // Nome do funcionário
        public string Position { get; set; } // Cargo do funcionário
        public decimal Wage { get; set; } // Salário base do funcionário
        public List<HoleriteItemModel> Itens { get; set; } = new(); // Lista de itens do holerite
        public decimal FullEarnings => Itens.Where(i => i.Type == "Provento").Sum(i => i.Value); // Soma dos proventos
        public decimal FullDiscounts => Itens.Where(i => i.Type == "Desconto").Sum(i => i.Value); // Soma dos descontos
        public decimal NetSalary => Wage + FullEarnings - FullDiscounts; // Cálculo do salário líquido
    }
}
