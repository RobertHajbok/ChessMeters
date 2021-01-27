using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ChessMeters.Core.Database
{
    public class ChessMetersContextFactory : IDesignTimeDbContextFactory<ChessMetersContext>
    {
        public ChessMetersContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            	.SetBasePath(Directory.GetCurrentDirectory())
		.AddJsonFile("appsettings.json")
		.AddEnvironmentVariables()
		.Build();
            var mySqlConnectionString = configuration.GetConnectionString("ChessMeters");
            var optionsBuilder = new DbContextOptionsBuilder<ChessMetersContext>();
            optionsBuilder.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString));

            return new ChessMetersContext(optionsBuilder.Options, new OperationalStoreOptionsMigrations());
        }
    }
}
