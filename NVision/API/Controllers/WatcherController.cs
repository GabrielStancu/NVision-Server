﻿using Infrastructure.DTOs;
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
        private readonly IWatcherService _watcherService;

        public WatcherController(IWatcherService watcherService)
        {
            _watcherService = watcherService;
        }

        [HttpGet("{watcherId}")]
        public async Task<ActionResult<WatcherDashboardDataDto>> GetWatcherDashboardData(int watcherId)
        {
            var watcherDashboardDataDto = await _watcherService.GetWatcherDashboardDataAsync(watcherId);
            return Ok(watcherDashboardDataDto);
        }

        [HttpGet("alerts/{watcherId}")]
        public async Task<ActionResult<IEnumerable<AlertDto>>> GetWatcherAlerts(int watcherId)
        {
            var alerts = await _watcherService.GetWatcherAlertsAsync(watcherId);
            return Ok(alerts);
        }

        [HttpPost("answer-alert")]
        public async Task<ActionResult<bool>> AnswerAlert(AlertAnswerDto alertAnswerDto)
        {
            bool answeredAlert = await _watcherService.AnswerAlertAsync(alertAnswerDto);
            return Ok(answeredAlert);
        }

        [HttpGet("subjects/{watcherId}")]
        public async Task<ActionResult<IEnumerable<SubjectWithoutMeasurementsDto>>> GetWatcherSubjects(int watcherId)
        {
            var subjects = await _watcherService.GetWatcherSubjectsAsync(watcherId);
            return Ok(subjects);
        }
    }
}
