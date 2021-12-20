using Infrastructure.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatcherController : ControllerBase
    {
        private readonly IWatcherDataService _watcherDataService;

        public WatcherController(IWatcherDataService watcherDataService)
        {
            _watcherDataService = watcherDataService;
        }

        [HttpPost("watcherData")]
        public async Task<ActionResult<IEnumerable<SubjectWithoutMeasurementsDto>>> GetWatcherHomepageData(WatcherHomepageDataRequestDto request)
        {
            var watcherHomepageData = await _watcherDataService.GetWatcherHomepageDataAsync(request);

            return Ok(watcherHomepageData);
        }

        [HttpPost("subjectData")]
        public async Task<ActionResult<SubjectWithMeasurementsReplyDto>> GetSubjectWithMeasurements(SubjectWithMeasurementsRequestDto request)
        {
            var subjectWithMeasurements = await _watcherDataService.GetSubjectWithMeasurementsAsync(request);

            return Ok(subjectWithMeasurements);
        }
    }
}
