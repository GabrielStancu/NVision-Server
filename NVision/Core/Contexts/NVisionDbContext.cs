using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Core.Contexts
{
    public class NVisionDbContext : DbContext
    {
        public NVisionDbContext(DbContextOptions<NVisionDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Subject> Subject { get; set; }
        public DbSet<Watcher> Watcher { get; set; }
        public DbSet<SensorMeasurement> SensorMeasurement { get; set; }
        public DbSet<Alert> Alert { get; set; }
        public DbSet<Device> Device { get; set; }
    }
}
