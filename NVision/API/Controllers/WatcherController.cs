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

        [HttpGet("{watcherId}")]
        public async Task<ActionResult<IEnumerable<SubjectWithoutMeasurementsDto>>> GetWatcherHomepageData(int watcherId)
        {
            var watcherHomepageData = await _watcherDataService.GetWatcherHomepageDataAsync(watcherId);

            return Ok(watcherHomepageData);
        }

        [HttpGet("subject/{subjectId}")]
        public async Task<ActionResult<SubjectWithMeasurementsDto>> GetSubjectWithMeasurements(int subjectId)
        {
            var subjectWithMeasurements = await _watcherDataService.GetSubjectWithMeasurementsAsync(subjectId);

            return Ok(subjectWithMeasurements);
        }
    }
}
