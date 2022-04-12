using Core.Contexts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ISensorMeasurementRepository : IGenericRepository<SensorMeasurement>
    {
        Task<int> GetWatcherMeasurementsCountLastDaysAsync(int watcherId, int days = 7);
        Task<int> GetMeasuredSubjectsCountAsync(IEnumerable<Subject> subjects);
        Task<IEnumerable<SensorMeasurement>> GetFilteredMeasurementsAsync(IEnumerable<SensorType> sensorTypes, DateTime startDate, DateTime endDate);
    }

    public class SensorMeasurementRepository : GenericRepository<SensorMeasurement>, ISensorMeasurementRepository
    {
        public SensorMeasurementRepository(NVisionDbContext context) : base(context)
        {
        }

        public async Task<int> GetWatcherMeasurementsCountLastDaysAsync(int watcherId, int days = 7)
        {
            var measurements = await Context.SensorMeasurement
                .Include(sm => sm.Subject)
                .Where(sm => sm.Subject.WatcherId == watcherId && sm.Timestamp.AddDays(days) >= DateTime.Now)
                .ToListAsync();

            return measurements.Count;
        }

        public async Task<int> GetMeasuredSubjectsCountAsync(IEnumerable<Subject> subjects)
        {
            int count = 0;
            foreach (var subject in subjects)
            {
                var measurements = await Context.SensorMeasurement
                                       .Where(sm => sm.SubjectId == subject.Id && sm.Timestamp.Date == DateTime.Now.Date) 
                                       .ToListAsync();
                if (measurements.Count > 0)
                    count++;
            }
            return count;
        }

        public async Task<IEnumerable<SensorMeasurement>> GetFilteredMeasurementsAsync(IEnumerable<SensorType> sensorTypes, DateTime startDate, DateTime endDate)
        {
            var measurements = await Context.SensorMeasurement
                .Where(m => m.Timestamp.Date >= startDate.Date && m.Timestamp.Date <= endDate.Date &&
                       sensorTypes.ToList().Contains(m.SensorType))
                .ToListAsync();

            return measurements;
        }
    }  
}
