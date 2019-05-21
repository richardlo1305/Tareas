using EntityFramework.ClasesEntidad;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class TareasDbContext : DbContext
    {
        public TareasDbContext(DbContextOptions<TareasDbContext> options) : base(options)
        {
        }

        public DbSet<Tarea> Tarea { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(t => t.Codigo);
            modelBuilder.Entity<Tarea>().HasKey(t => t.Codigo);
            modelBuilder.Entity<Tarea>().Property(t => t.Codigo).ValueGeneratedOnAdd();
            modelBuilder.Entity<Usuario>().Property(t => t.Codigo).ValueGeneratedOnAdd();

            modelBuilder.Entity<Usuario>()
                .Property(t => t.Identificacion)
                .IsRequired();
        }
    }
}
