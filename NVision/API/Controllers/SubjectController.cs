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
        [HttpGet("health-data/{subjectId}")]
        public async Task<ActionResult<object>> GetHealthData(int subjectId)
        {
            await Task.Delay(10);
            return null;
        }
    }
}
