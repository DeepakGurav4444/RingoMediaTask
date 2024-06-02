using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Models.Entities;

namespace RingoMediaTask.Data
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Management> Managements { get; set; }
        public DbSet<Operational> Operationals { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
    }
}
