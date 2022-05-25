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
        Task<IEnumerable<Alert>> GetWatcherAlertsAsync(int watcherId);
        Task<IEnumerable<Alert>> GetWatcherDashboardAlertsAsync(int watcherId);
        Task<int> GetWatcherAlertsCountAsync(int watcherId);
        Task<bool> AnswerAlertAsync(int alertId, bool wasTrueAlert);
    }

    public class AlertRepository : GenericRepository<Alert>, IAlertRepository
    {
        private const int _watcherDashboardAlertsCount = 5;
        public AlertRepository(NVisionDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Alert>> GetWatcherAlertsAsync(int watcherId)
        {
            var alerts = await Context.Alert
                .Include(a => a.Subject)
                .Where(a => a.Subject.WatcherId == watcherId)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
            return alerts;
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

        public async Task<int> GetWatcherAlertsCountAsync(int watcherId)
        {
            var alerts = await Context.Alert
                .Include(a => a.Subject)
                .Where(a => a.Subject.WatcherId == watcherId && a.Timestamp.Date == System.DateTime.Now.Date)
                .ToListAsync();

            return alerts.Count;
        }

        public async Task<bool> AnswerAlertAsync(int alertId, bool wasTrueAlert)
        {
            var alert = await Context.Alert
                .FirstOrDefaultAsync(a => a.Id == alertId);
            alert.WasTrueAlert = wasTrueAlert;
            await UpdateAsync(alert);

            return true;
        }
    }
}
