using POCApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace POCApi.Infrastructure.Contexts
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        
        public DbSet<CompartmentPick> CompartmentPicks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connStr = _configuration.GetConnectionString("DemoConnection");
            optionsBuilder.UseNpgsql(connStr).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}