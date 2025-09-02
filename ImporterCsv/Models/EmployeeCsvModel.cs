namespace ImporterCsv.Models
{
    public class EmployeeCsvModel
    {
        public int Id { get; set; } // id do funcionário
        public string Name { get; set; } // nome do funcionário
        public string Position { get; set; } // cargo do funcionário
        public decimal Wage { get; set; } // salário do funcionário
    }
}
