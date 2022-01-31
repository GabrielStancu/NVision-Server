using Core.Contexts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IAlertRepository : IGenericRepository<Alert>
    {
        Task<IEnumerable<Alert>> GetWatcherDashboardAlertsAsync(int watcherId);
        Task<int> GetWatcherAlertsCountAsync(int watcherId, int days = 7);
    }

    public class AlertRepository : GenericRepository<Alert>, IAlertRepository
    {
        private const int _watcherDashboardAlertsCount = 5;
        public AlertRepository(NVisionDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Alert>> GetWatcherDashboardAlertsAsync(int watcherId)
        {
            var alerts = await Context.Alert
                .Include(a => a.Subject)
                .Where(a => a.Subject.WatcherId == watcherId)
                .OrderByDescending(a => a.Timestamp)
                .Take(_watcherDashboardAlertsCount)
                .ToListAsync();
            return alerts;
        }

        public async Task<int> GetWatcherAlertsCountAsync(int watcherId, int days = 7)
        {
            var alerts = await Context.Alert
                .Include(a => a.Subject)
                .Where(a => a.Subject.WatcherId == watcherId)
                .ToListAsync();

            return alerts.Count;
        }
    }
}
