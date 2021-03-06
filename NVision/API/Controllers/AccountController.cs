using API.Security;
using Infrastructure.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(
            ILoginService loginService,
            IRegisterService registerService,
            IUserService userService,
            IWebHostEnvironment webHostEnvironment)
        {
            _loginService = loginService;
            _registerService = registerService;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResultDto>> Login(LoginRequestDto loginRequestDto)
        {
            var loginResultDto = await _loginService.LoginAsync(loginRequestDto);

            if (loginResultDto != null)
            {
                loginResultDto.Token = TokenGenerator.GenerateToken(loginResultDto.Username, loginResultDto.UserType);
                return Ok(loginResultDto);
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register(UserRegisterRequestDto userRegisterRequestDto)
        {
            bool registeredWatcher = await _registerService.RegisterUserAsync(userRegisterRequestDto);
            return Ok(registeredWatcher);
        }

        [HttpPost("saveFile")]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files.First();
                string filename = postedFile.FileName;
                var physicalPath = _webHostEnvironment.ContentRootPath + "/ProfilePictures/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult(string.Empty);
            }
        }

        [HttpPost("displayData")]
        public async Task<ActionResult<UserDisplayDataDto>> GetDisplayData(UserDisplayDataRequestDto request)
        {
            var userDisplayData = await _userService.GetUserDisplayDataAsync(request);

            if (userDisplayData is not null)
                return Ok(userDisplayData);
            return NotFound("User not found");
        }
    }
}
