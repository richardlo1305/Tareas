using EntityFramework.ClasesEntidad;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class TareasDbContext : DbContext
    {
        public TareasDbContext(DbContextOptions<TareasDbContext> options) : base(options)
        {
        }

        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .Property(t => t.Identificacion)
                .IsRequired();
        }
    }
}
