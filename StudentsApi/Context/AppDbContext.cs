using Microsoft.EntityFrameworkCore;
using StudentsApi.Models;

namespace StudentsApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                    new Student { Id = 1, Name = "John", Age = 18, Email = "john@doe.com" },
                    new Student
                    {
                        Id = 2,
                        Name = "Jane",
                        Age = 20,
                        Email = "jane@email.com"
                    }
                );
        }
    }
}
