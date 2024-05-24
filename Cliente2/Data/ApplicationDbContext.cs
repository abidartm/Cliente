using Microsoft.EntityFrameworkCore;
using Cliente2.Models;

namespace Cliente2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Db/Cliente.db");
        }

        // Definir DbSet para cada entidad
        public DbSet<ModelCliente> cliente { get; set; }

    }
}
