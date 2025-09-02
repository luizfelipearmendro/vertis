using CsvHelper;
using ImporterCsv.Models;
using System.Formats.Asn1;
using System.Globalization;

namespace ImporterCsv.Services
{
    public class CsvProcessor
    {
        private readonly HttpClient _httpClient;

        public CsvProcessor(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public async Task<string> ProcessEmployeesCsv(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var employees = csv.GetRecords<EmployeeCsvModel>().ToList();

            foreach (var f in employees)
            {
                await _httpClient.PostAsJsonAsync("http://localhost:5001/api/employee", f);
            }

            return $"Imported {employees.Count} employees.";
        }

        public async Task<string> ProcessEventsCsv(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var events = csv.GetRecords<EventCsvModel>().ToList();

            foreach (var e in events)
            {
                await _httpClient.PostAsJsonAsync("http://localhost:5002/api/event", e);
            }

            return $"Imported {events.Count} events.";
        }
    }
}
