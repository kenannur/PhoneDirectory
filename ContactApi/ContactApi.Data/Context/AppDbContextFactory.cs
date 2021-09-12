using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ContactApi.Data.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath("/Users/kenannur/GitHub/PhoneDirectory/ContactApi/ContactApi")
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptionsBuilder.UseNpgsql(configurationRoot.GetConnectionString("Default"));

            return new AppDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
