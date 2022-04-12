using Infrastructure.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<ActionResult<SubjectDeviceDataDto>> GetSubjectData(Guid serialNumber)
        {
            var subjectData = await _deviceService.GetSubjectDeviceDataAsync(serialNumber);
            return Ok(subjectData);
        }
    }
}
