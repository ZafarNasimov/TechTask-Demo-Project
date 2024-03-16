using Microsoft.EntityFrameworkCore;
using SharedLib;

namespace ProductsAPI.Data
{
    public class AppDbContext : DbContext
    {
        private string _connectionString;

        public DbSet<Product> Product { get; set; }

        public AppDbContext()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
