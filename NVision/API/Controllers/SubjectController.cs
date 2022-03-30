using Infrastructure.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [Authorize(Roles = "Subject")]
        [HttpGet("profile-data/{subjectId}")]
        public async Task<ActionResult<SubjectProfileDataDto>> GetProfileData(int subjectId)
        {
            var subjectProfileData = await _subjectService.GetProfileDataAsync(subjectId);
            return Ok(subjectProfileData);
        }

        [Authorize(Roles = "Subject")]
        [HttpPut("save-changes")]
        public async Task<ActionResult<bool>> SaveChanges(SubjectProfileDataDto subjectProfileDataDto)
        {
            bool savedChanges = await _subjectService.SaveChangesAsync(subjectProfileDataDto);
            return Ok(savedChanges);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<MeasurementsReplyDto>> GetHealthData(MeasurementsRequestDto request)
        {
            var reply = await _subjectService.GetMeasurementsAsync(request);
            return Ok(reply);
        }

        [Authorize(Roles = "Watcher")]
        [HttpPost("serial-number")]
        public async Task<ActionResult<bool>> UpdateDeviceSerialNumber(UpdateSerialNumberRequest request)
        {
            var reply = await _subjectService.UpdateSerialNumberAsync(request);
            return Ok(reply);
        }
    }
}
