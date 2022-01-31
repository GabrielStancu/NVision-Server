using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IWatcherService
    {
        Task<WatcherDashboardDataDto> GetWatcherDashboardDataAsync(int watcherId);
    }

    public class WatcherService : IWatcherService
    {
        private readonly IWatcherDashboardService _watcherDashboardService;

        public WatcherService(IWatcherDashboardService watcherDashboardService)
        {
            _watcherDashboardService = watcherDashboardService;
        }
        public async Task<WatcherDashboardDataDto> GetWatcherDashboardDataAsync(int watcherId)
        {
            var dashboardData = await _watcherDashboardService.GetWatcherDashboardDataAsync(watcherId);
            return dashboardData;
        }
    }
}
