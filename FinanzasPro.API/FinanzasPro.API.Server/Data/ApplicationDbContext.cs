using Microsoft.EntityFrameworkCore;

namespace ProyectoFinanzas
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<TarjetaCredito> Tarjetas { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Ingreso> Ingresos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=FinanzasProDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apagamos el borrado en cascada automático entre Categoría e Ingreso
            modelBuilder.Entity<Ingreso>()
                .HasOne(i => i.Categoria)
                .WithMany(c => c.Ingresos)
                .HasForeignKey(i => i.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Hacemos lo mismo para Categoría y Gasto
            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Categoria)
                .WithMany(c => c.Gastos)
                .HasForeignKey(g => g.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}