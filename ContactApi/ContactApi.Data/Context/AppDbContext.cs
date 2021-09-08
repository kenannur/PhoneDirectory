using ContactApi.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ContactApi.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        static AppDbContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<ContactType>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<ContactType>();
        }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ContactInformation> ContactInformations { get; set; }
    }
}
