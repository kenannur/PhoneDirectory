using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ContactInformationApi.Data.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath("/Users/kenannur/GitHub/PhoneDirectory/ContactInformationApi/ContactInformationApi")
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptionsBuilder.UseNpgsql(configurationRoot.GetConnectionString("Default"));

            return new AppDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
