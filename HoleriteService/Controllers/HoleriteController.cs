using HoleriteService.Documents;
using HoleriteService.Models;
using HoleriteService.Services;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;

namespace HoleriteService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HoleriteController : Controller
    {
        private readonly HoleriteGenerator _generator;

        public HoleriteController(HoleriteGenerator generator)
        {
            _generator = generator;
        }

        // Retorna o holerite em JSON
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<HoleriteModel>> Get(int employeeId)
        {
            var holerite = await _generator.GerarHolerite(employeeId);
            if (holerite == null)
                return NotFound($"Employee {employeeId} not found.");

            return Ok(holerite);
        }

        // Retorna o holerite em PDF
        [HttpGet("{employeeId}/pdf")]
        public async Task<IActionResult> GetPdf(int employeeId)
        {
            var holerite = await _generator.GerarHolerite(employeeId);
            if (holerite == null)
                return NotFound($"Employee {employeeId} not found.");

            var document = new HoleriteDocument(holerite);
            var pdfBytes = document.GeneratePdf();

            return File(pdfBytes, "application/pdf", $"holerite_{employeeId}.pdf");
        }
    }
}
