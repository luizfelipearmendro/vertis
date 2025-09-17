using System;
using EventService.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // MySqlServerVersion
using Microsoft.AspNetCore.HttpOverrides;

namespace EventService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Lê do appsettings.json ou de variável de ambiente (ex.: em PaaS)
            var cs = builder.Configuration.GetConnectionString("DefaultConnection")
                     ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

            // Fixa versão compatível (evita AutoDetect conectar durante 'migrations add')
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(cs, serverVersion, o => o.EnableRetryOnFailure()));

            // CORS: defina "AllowedOrigin" com a URL do seu front em produção
            var allowedOrigin = builder.Configuration["AllowedOrigin"];
            builder.Services.AddCors(opt =>
                opt.AddDefaultPolicy(p =>
                {
                    if (!string.IsNullOrWhiteSpace(allowedOrigin))
                        p.WithOrigins(allowedOrigin).AllowAnyHeader().AllowAnyMethod();
                    else
                        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); // liberado em dev
                }));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // Opcional: aplica migrations automaticamente em DEV
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            // Suporte a proxies (Render/EB/Nginx) para URLs/HTTPS corretos
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
