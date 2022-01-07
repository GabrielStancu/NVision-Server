using Core.Contexts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class AlertRepository : GenericRepository<Alert>, IAlertRepository
    {
        private const int _watcherDashboardAlertsCount = 5;
        public AlertRepository(NVisionDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Alert>> GetUnansweredWatcherAlerts(int watcherId)
        {
            return await Context.Alert
                .Where(a => a.WatcherId == watcherId && a.WasTrueAlert == null)
                .ToListAsync();
        }

        public async Task<Alert> AnswerAndGetNextAlertAsync(int alertId, bool wasAlertAccurate)
        {
            var alert = await SelectByIdAsync(alertId);
            alert.WasTrueAlert = wasAlertAccurate;
            await UpdateAsync(alert);

            var nextAlert = await Context.Alert
                .FirstOrDefaultAsync(a => a.WasTrueAlert == null && a.Id < alertId);
            return nextAlert;
        }
        
        ////
        public async Task<IEnumerable<Alert>> GetWatcherDashboardAlertsAsync(int watcherId)
        {
            var alerts = await Context.Alert
                .Where(a => a.WatcherId == watcherId)
                .OrderByDescending(a => a.Timestamp)
                .Take(_watcherDashboardAlertsCount)
                .ToListAsync();
            return alerts;
        }
    }
}
