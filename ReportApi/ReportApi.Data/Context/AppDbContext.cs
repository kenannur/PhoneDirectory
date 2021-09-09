using Microsoft.EntityFrameworkCore;
using Npgsql;
using ReportApi.Shared.Entities;

namespace ReportApi.Data.Context
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

        public DbSet<ContactInformation> ContactInformations { get; set; }
    }
}
