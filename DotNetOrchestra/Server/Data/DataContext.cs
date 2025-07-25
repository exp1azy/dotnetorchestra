using DynamicDI;
using Microsoft.EntityFrameworkCore;

namespace DotNetOrchestra.Server.Data
{
    [RegisterDbContext]
    public class DataContext : DbContext
    {
        private readonly IConfiguration _config;

        public DataContext(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config.GetConnectionString("Default"));
        }
    }
}
