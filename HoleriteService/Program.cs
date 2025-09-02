using HoleriteService.Clients;
using HoleriteService.Services;
using QuestPDF.Infrastructure;

namespace HoleriteService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Definir licen�a do QuestPDF
            QuestPDF.Settings.License = LicenseType.Community;

            // Configura��es padr�o
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "HoleriteService API",
                    Description = "API para gera��o de holerites"
                });
            });

            // Clients e servi�os
            builder.Services.AddHttpClient<EmployeeClient>();
            builder.Services.AddHttpClient<EventClient>();
            builder.Services.AddScoped<HoleriteGenerator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HoleriteService v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}