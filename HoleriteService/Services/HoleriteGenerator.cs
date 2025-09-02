using HoleriteService.Clients;
using HoleriteService.Models;

namespace HoleriteService.Services
{
    public class HoleriteGenerator
    {
        private readonly EmployeeClient _employeeClient;
        private readonly EventClient _eventClient;

        public HoleriteGenerator(EmployeeClient employeeClient, EventClient eventClient)
        {
            _employeeClient = employeeClient;
            _eventClient = eventClient;
        }

        public async Task<HoleriteModel> GerarHolerite(int employeeId)
        {
            var employee = await _employeeClient.GetEmployeeAsync(employeeId);
            var events = await _eventClient.GetEventAsync(employeeId);

            var holerite = new HoleriteModel
            {
                EmployeeId = employee.Id,
                Name = employee.Name,
                Position = employee.Position,
                Wage = employee.Wage
            };

            foreach (var e in events)
            {
                holerite.Itens.Add(new HoleriteItemModel
                {
                    Description = e.Type,
                    Type = e.Type == "Desconto" ? "Desconto" : "Provento",
                    Value = e.Value
                });
            }

            return holerite;
        }
    }
}
