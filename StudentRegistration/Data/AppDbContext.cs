using Microsoft.EntityFrameworkCore;
using StudentRegistration.Models;

namespace StudentRegistration.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                "Data Source=ASTERIS001;Initial Catalog=demodb;User Id=sa;Password=Asteris@123;TrustServerCertificate=True;");
            }
        }
        public DbSet<Student> Students { get; set; }
    }
}
