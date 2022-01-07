using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IWatcherDashboardService
    {
        Task<WatcherDashboardDataDto> GetWatcherDashboardDataAsync(int watcherId);
    }
}