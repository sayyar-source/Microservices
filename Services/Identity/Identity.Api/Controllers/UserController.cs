
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Identity.Identity.Application.Service;
using Services.Identity.Identity.Data.Dtos;
using Services.Identity.Identity.Data.Models;

namespace Services.Identity.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;


        public UserController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public ActionResult Login(UserLoginDto userModel)
        {
            if (string.IsNullOrEmpty(userModel.Email) || string.IsNullOrEmpty(userModel.Password))
            {
                return RedirectToAction("Error");
            }

            IActionResult response = Unauthorized();

            var generatedToken = _tokenService.BuildToken(userModel);

            return Ok(new
            {
                token = generatedToken,
                email = userModel.Email
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public ActionResult UserRegister(UserDto userModel)
        {
            try
            {
                if (string.IsNullOrEmpty(userModel.Email) || string.IsNullOrEmpty(userModel.Password))
                {
                    return RedirectToAction("Error");
                }
                var user = _userService.AddUser(userModel);
                return Ok(user);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("generatPassword")]
        public ActionResult<IEnumerable<string>> GeneratPassword()
        {
            var pass = _userService.GeneratePassword();
            return Ok(pass);
        }

        [HttpGet]
        [Route("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userService.GetAllUsers();
            return Ok(user);
        }

        [HttpGet]
        [Route("get-user-by-email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            var user = await _userService.GetUserByEmail(email);
            return Ok(user);
        }
        [HttpPut]
        [Route("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userModel)
        {
            var result = await _userService.UpdateUser(userModel);
            return Ok(result);
        }
    }
}
