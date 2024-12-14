using Microsoft.EntityFrameworkCore;
using MyApplication.Domain.Entities;

namespace MyApplication.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
    }
}
