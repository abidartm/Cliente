using Microsoft.EntityFrameworkCore;
using Cliente2.Models;
using Microsoft.Extensions.Logging;

namespace Cliente2.Data
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        // : base(options)
        //{
        //}
        //
        private readonly ILoggerFactory _loggerFactory;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Db/Cliente.db");
            optionsBuilder.UseLoggerFactory(_loggerFactory); // Usar el logger factory
            optionsBuilder.EnableSensitiveDataLogging(); // Habilitar logging de datos sensibles si es necesario
            base.OnConfiguring(optionsBuilder);
        }

        // Definir DbSet para cada entidad
        public DbSet<ModelCliente> cliente { get; set; }

    }
}
