using HoleriteService.Dtos;

namespace HoleriteService.Clients
{
    public class EmployeeClient
    {
        private readonly HttpClient _httpClient;

        public EmployeeClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmployeeDto?> GetEmployeeAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeDto>(
                $"http://localhost:5001/api/employee/{id}");
        }
    }
}
