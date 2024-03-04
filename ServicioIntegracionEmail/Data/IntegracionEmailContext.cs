using Microsoft.EntityFrameworkCore;
using ServicioIntegracionEmail.Models.Entity;

namespace ServicioIntegracionEmail.Data
{
    public class IntegracionEmailContext : DbContext
    {
        public IntegracionEmailContext(DbContextOptions<IntegracionEmailContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<UserRol> UsuarioRol { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRol>()
                .HasKey(ur => new { ur.idusuario, ur.idrol }); // Configuración de clave primaria compuesta
        }

    }
}
