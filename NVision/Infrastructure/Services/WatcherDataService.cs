using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class WatcherDataService : IWatcherDataService
    {
        private readonly IWatcherDashboardService _watcherDashboardService;

        public WatcherDataService(IWatcherDashboardService watcherDashboardService)
        {
            _watcherDashboardService = watcherDashboardService;
        }

        public async Task<WatcherDashboardDataDto> GetWatcherDashboardDataAsync(int watcherId)
        {
            var watcherDashboardDataDto = await _watcherDashboardService.GetWatcherDashboardDataAsync(watcherId);
            return watcherDashboardDataDto;
        }
    }
}
