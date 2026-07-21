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
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Telemetry> Telemetries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Telemetry>()
                .HasIndex(t => new { t.FlightId, t.Timestamp });

            modelBuilder.Entity<Flight>()
                .HasIndex(d => new { d.DroneId }); 
        }
    }
}