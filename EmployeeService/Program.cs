using System;
using EmployeeService.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // MySqlServerVersion

namespace EmployeeService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Lê a connection string do appsettings.json OU de variável de ambiente
            // (útil em PaaS como Render/AWS, etc.)
            var cs = builder.Configuration.GetConnectionString("DefaultConnection")
                     ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

            // Evita AutoDetect (que tenta conectar até para "migrations add").
            // TiDB é compatível com MySQL 8.x. Ajuste se necessário.
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(cs, serverVersion));

            // CORS: em produção, defina "AllowedOrigin" para o domínio do seu front
            var allowedOrigin = builder.Configuration["AllowedOrigin"];
            builder.Services.AddCors(opt =>
                opt.AddDefaultPolicy(p =>
                {
                    if (!string.IsNullOrWhiteSpace(allowedOrigin))
                        p.WithOrigins(allowedOrigin).AllowAnyHeader().AllowAnyMethod();
                    else
                        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); // dev
                }));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // (Opcional) Aplicar migrations automaticamente só em DEV
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
