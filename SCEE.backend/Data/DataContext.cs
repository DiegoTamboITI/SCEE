using Microsoft.EntityFrameworkCore;
using SCEE.Shared.Entities;

namespace SCEE.backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
