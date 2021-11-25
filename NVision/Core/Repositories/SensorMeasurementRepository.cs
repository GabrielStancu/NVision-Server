using Core.Contexts;
using Core.Models;

namespace Core.Repositories
{
    public class SensorMeasurementRepository : GenericRepository<SensorMeasurement>, ISensorMeasurementRepository
    {
        public SensorMeasurementRepository(NVisionDbContext context) : base(context)
        {
        }
    }
}
