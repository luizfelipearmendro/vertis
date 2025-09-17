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

            // L� a connection string do appsettings.json OU de vari�vel de ambiente
            // (�til em PaaS como Render/AWS, etc.)
            var cs = builder.Configuration.GetConnectionString("DefaultConnection")
                     ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

            // Evita AutoDetect (que tenta conectar at� para "migrations add").
            // TiDB � compat�vel com MySQL 8.x. Ajuste se necess�rio.
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(cs, serverVersion));

            // CORS: em produ��o, defina "AllowedOrigin" para o dom�nio do seu front
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

                // (Opcional) Aplicar migrations automaticamente s� em DEV
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
