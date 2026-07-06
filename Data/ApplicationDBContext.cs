using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using drone_dashboard_1.Models;

namespace drone_dashboard_1.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;  
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Drone> Drones { get; set; }
    }
}