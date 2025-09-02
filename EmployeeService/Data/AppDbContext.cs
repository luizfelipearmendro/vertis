﻿using EmployeeService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
