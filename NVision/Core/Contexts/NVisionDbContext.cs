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
        public DbSet<AirflowSensorMeasurement> AirflowSensorMeasurement { get; set; }
        public DbSet<BloodPressureSensorMeasurement> BloodPressureSensorMeasurement { get; set; }
        public DbSet<EcgSensorMeasurement> EcgSensorMeasurement { get; set; }
        public DbSet<GsrSensorMeasurement> GsrSensorMeasurement { get; set; }
        public DbSet<PulseOxygenHeartRateSensorMeasurement> PulseOxygenHeartRateSensorMeasurement { get; set; }
        public DbSet<TemperatureSensorMeasurement> TemperatureSensorMeasurement { get; set; }
        public DbSet<Alert> Alert { get; set; }
    }
}
