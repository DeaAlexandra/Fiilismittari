using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = null!;
        public DbSet<UserData> UserData { get; set; } = null!;
    }
}