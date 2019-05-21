using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EntityFramework.OperacionesBD
{
    public class TareasDbContextFactory : IDesignTimeDbContextFactory<TareasDbContext>
    {
        public TareasDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WebAPI"))
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<TareasDbContext>();
            var connectionString = configuration.GetConnectionString("Postgres");
            builder.UseNpgsql(connectionString);
            return new TareasDbContext(builder.Options);
        }
    }
}
