namespace HoleriteService.Dtos
{
    public class EventDto
    {
        public int Id { get; set; } // id do evento
        public int EmployeeId { get; set; } // id do funcionário
        public string Type { get; set; } // tipo do evento (e.g., "Desconto", "Provento")
        public decimal Value { get; set; } // valor do evento
        public DateTime Date { get; set; } // data do evento
    }
}
