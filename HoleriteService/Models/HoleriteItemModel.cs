namespace HoleriteService.Models
{
    public class HoleriteItemModel
    {
        public string Description { get; set; } // Descrição do item
        public string Type { get; set; } // "Provento" ou "Desconto"
        public decimal Value { get; set; } // Valor do item
    }
}
