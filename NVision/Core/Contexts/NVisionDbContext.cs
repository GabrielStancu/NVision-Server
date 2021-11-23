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

    }
}
