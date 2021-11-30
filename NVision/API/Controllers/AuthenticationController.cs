using Infrastructure.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;

        public AuthenticationController(
            ILoginService loginService,
            IRegisterService registerService)
        {
            _loginService = loginService;
            _registerService = registerService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResultDto>> Login(LoginRequestDto loginRequestDto)
        {
            var loginResultDto = await _loginService.LoginAsync(loginRequestDto);
            return Ok(loginResultDto);
        }

        [HttpPost("register-watcher")]
        public async Task<ActionResult<bool>> Register(WatcherRegisterRequestDto watcherRegisterRequestDto)
        {
            bool registeredWatcher = await _registerService.RegisterUserAsync(watcherRegisterRequestDto);
            return Ok(registeredWatcher);
        }

        [HttpPost("register-subject")]
        public async Task<ActionResult<bool>> Register(SubjectRegisterRequestDto subjectRegisterRequestDto)
        {
            bool registeredSubject = await _registerService.RegisterUserAsync(subjectRegisterRequestDto);
            return Ok(registeredSubject);
        }
    }
}
