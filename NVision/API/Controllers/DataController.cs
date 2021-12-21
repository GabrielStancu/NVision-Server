using Infrastructure.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IWatcherDataService _watcherDataService;
        private readonly ISubjectDataService _subjectDataService;

        public DataController(IWatcherDataService watcherDataService, ISubjectDataService subjectDataService)
        {
            _watcherDataService = watcherDataService;
            _subjectDataService = subjectDataService;
        }

        [HttpPost("watcher")]
        public async Task<ActionResult<IEnumerable<SubjectWithoutMeasurementsDto>>> GetWatcherHomepageData(WatcherHomepageDataRequestDto request)
        {
            var watcherHomepageData = await _watcherDataService.GetWatcherHomepageDataAsync(request);

            return Ok(watcherHomepageData);
        }

        [HttpPost("subject")]
        public async Task<ActionResult<SubjectWithMeasurementsReplyDto>> GetSubjectWithMeasurements(SubjectWithMeasurementsRequestDto request)
        {
            var subjectWithMeasurements = await _subjectDataService.GetSubjectWithMeasurementsAsync(request);

            return Ok(subjectWithMeasurements);
        }

        [HttpPost("alert")]
        public async Task<ActionResult> AnswerAndGetNextAlert(AlertAnswerDto alertAnswerDto)
        {
            var nextAlert = await _watcherDataService.AnswerAndGetNextAlertAsync(alertAnswerDto);

            return Ok(nextAlert);
        }
    }
}
