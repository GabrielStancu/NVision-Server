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

        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register(UserRegisterRequestDto userRegisterRequestDto)
        {
            bool registeredWatcher = await _registerService.RegisterUserAsync(userRegisterRequestDto);
            return Ok(registeredWatcher);
        }
    }
}
