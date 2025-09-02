namespace ImporterCsv.Models
{
    public class EventCsvModel
    {
        public int EmployeeId { get; set; } // id do funcionário
        public string Type { get; set; } // Ex: "HorasExtras", "Desconto", "Bonus"
        public decimal Value { get; set; } // valor do evento
        public DateTime Date { get; set; } // data do evento
    }
}
