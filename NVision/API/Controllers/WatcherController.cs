using Infrastructure.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatcherController : ControllerBase
    {
        private readonly IWatcherService _watcherDataService;

        public WatcherController(IWatcherService watcherDataService)
        {
            _watcherDataService = watcherDataService;
        }

        [HttpGet("{watcherId}")]
        public async Task<ActionResult<WatcherDashboardDataDto>> GetWatcherDashboardData(int watcherId)
        {
            var watcherDashboardDataDto = await _watcherDataService.GetWatcherDashboardDataAsync(watcherId);
            return Ok(watcherDashboardDataDto);
        }
    }
}
