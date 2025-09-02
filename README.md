# Vertis 💼  
Sistema distribuído para geração de holerites com arquitetura baseada em microsserviços.

## 📦 Visão Geral

O projeto **Vertis** é composto por quatro microsserviços independentes que se comunicam via HTTP:

- `Vertis-FuncionariosService`: Gerencia dados dos funcionários
- `Vertis-EventoService`: Registra eventos financeiros (horas extras, descontos, bônus)
- `Vertis-HoleriteService`: Calcula o salário líquido e gera holerites em PDF
- `Vertis-ImportadorCsv`: Importa dados via arquivos CSV e distribui para os serviços

---

## 🧱 Arquitetura

Cada serviço é um projeto ASP.NET Core Web API, com banco de dados local via Entity Framework Core. A comunicação entre serviços é feita por HTTP Clients com DTOs desacoplados.

```plaintext
[ImportadorCsv] → POST → [FuncionarioService]
                → POST → [EventoService]

[HoleriteService] → GET → [FuncionarioService]
                 → GET → [EventoService]
                 → PDF → Swagger ou download
