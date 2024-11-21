using Microsoft.EntityFrameworkCore;
using BackendProject.Models;

namespace BackendProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserData> UserDatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=backend.db"); // Sama tietokanta
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>()
                .Property(e => e.Date)
                .HasConversion(
                    v => v.ToString("yyyy-MM-dd"),
                    v => DateOnly.Parse(v));
        }
    }
}