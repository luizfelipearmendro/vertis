using ImporterCsv.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImporterCsv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly CsvProcessor _csvProcessor;

        public UploadController(CsvProcessor csvProcessor)
        {
            _csvProcessor = csvProcessor;
        }

        [HttpPost("employees")]
        public async Task<IActionResult> UploadEmployees(IFormFile file)
        {
            var result = await _csvProcessor.ProcessEmployeesCsv(file);
            return Ok(result);
        }

        [HttpPost("events")]
        public async Task<IActionResult> UploadEvents(IFormFile file)
        {
            var result = await _csvProcessor.ProcessEventsCsv(file);
            return Ok(result);
        }
    }
}
