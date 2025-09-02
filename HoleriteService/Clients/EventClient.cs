using HoleriteService.Dtos;

namespace HoleriteService.Clients
{
    public class EventClient
    {
        private readonly HttpClient _httpClient;

        public EventClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EventDto>> GetEventAsync(int employeeId)
        {
            var events = await _httpClient.GetFromJsonAsync<List<EventDto>>(
                $"http://localhost:5002/api/event?employeeId={employeeId}");
            return events ?? new ();
        }
    }
}
