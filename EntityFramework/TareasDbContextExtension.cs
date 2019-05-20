using EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TareasDbContextExtension
    {
        public static IServiceCollection AddTareasDbContext(this IServiceCollection services, string ConnectionString)
        {
            return services.AddDbContext<TareasDbContext>(options => options.UseNpgsql(ConnectionString));
        }
    }
}
