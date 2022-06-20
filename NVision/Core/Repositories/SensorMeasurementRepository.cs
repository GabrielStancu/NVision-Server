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
        Task<int> GetWatcherMeasurementsCountLastDaysAsync(int watcherId, DateTime crtDate, int days = 1);
        Task<int> GetMeasuredSubjectsCountAsync(IEnumerable<Subject> subjects, DateTime crtDate);
        Task<IEnumerable<SensorMeasurement>> GetFilteredMeasurementsAsync(IEnumerable<SensorType> sensorTypes, int subjectId, DateTime startDate, DateTime endDate);
    }

    public class SensorMeasurementRepository : GenericRepository<SensorMeasurement>, ISensorMeasurementRepository
    {
        public SensorMeasurementRepository(NVisionDbContext context) : base(context)
        {
        }

        public async Task<int> GetWatcherMeasurementsCountLastDaysAsync(int watcherId, DateTime crtDate, int days = 1)
        {
            var measurements = await Context.SensorMeasurement
                .Include(sm => sm.Subject)
                .Where(sm => sm.Subject.WatcherId == watcherId && sm.Timestamp.AddDays(days) >= crtDate)
                .ToListAsync();

            return measurements.Count;
        }

        public async Task<int> GetMeasuredSubjectsCountAsync(IEnumerable<Subject> subjects, DateTime crtDate)
        {
            int count = 0;
            foreach (var subject in subjects)
            {
                var hasMeasurements = await Context.SensorMeasurement
                                       .AnyAsync(sm => sm.SubjectId == subject.Id && sm.Timestamp.AddDays(1) >= crtDate);
                if (hasMeasurements)
                    count++;
            }
            return count;
        }

        public async Task<IEnumerable<SensorMeasurement>> GetFilteredMeasurementsAsync(IEnumerable<SensorType> sensorTypes, int subjectId, DateTime startDate, DateTime endDate)
        {
            var measurements = await Context.SensorMeasurement
                .Where(m => m.Timestamp.Date >= startDate.Date && m.Timestamp.Date <= endDate.Date &&
                       sensorTypes.ToList().Contains(m.SensorType) && m.SubjectId == subjectId)
                .ToListAsync();

            return measurements;
        }
    }  
}
