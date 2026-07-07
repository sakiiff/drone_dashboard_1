using Microsoft.EntityFrameworkCore;
using drone_dashboard_1.Models;

namespace drone_dashboard_1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Drone> Drones { get; set; }
    }
}