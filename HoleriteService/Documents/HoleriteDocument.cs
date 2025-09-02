using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using HoleriteService.Models;

namespace HoleriteService.Documents
{
    public class HoleriteDocument : IDocument
    {
        private readonly HoleriteModel _holerite;

        public HoleriteDocument(HoleriteModel holerite)
        {
            _holerite = holerite;
        }

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.Content().Column(col =>
                {
                    col.Item().Text($"Holerite - {_holerite.Name}").FontSize(20).Bold();
                    col.Item().Text($"Cargo: {_holerite.Position}");
                    col.Item().Text($"Salário Base: R$ {_holerite.Wage:F2}");

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn();
                            c.ConstantColumn(100);
                            c.ConstantColumn(100);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Descrição").Bold();
                            header.Cell().Text("Tipo").Bold();
                            header.Cell().Text("Valor").Bold();
                        });

                        foreach (var item in _holerite.Itens)
                        {
                            table.Cell().Text(item.Description);
                            table.Cell().Text(item.Type);
                            table.Cell().Text($"R$ {item.Value:F2}");
                        }
                    });

                    col.Item().Text($"Salário Líquido: R$ {_holerite.NetSalary:F2}")
                        .FontSize(16).Bold().FontColor(Colors.Green.Medium);
                });
            });
        }
    }
}
