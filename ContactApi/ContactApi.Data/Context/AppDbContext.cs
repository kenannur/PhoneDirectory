using ContactApi.Data.Faker;
using ContactApi.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactApi.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
