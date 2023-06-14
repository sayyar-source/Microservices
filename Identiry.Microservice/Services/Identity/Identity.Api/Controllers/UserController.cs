
using ManageMoney.Application.Services;
using ManageMoney.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageMoney.Api.Controllers
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
        public ActionResult Login( UserLoginDto userModel)
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
        public ActionResult UserRegister(UserRegisterDto userModel)
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
            var pass=_userService.GeneratePassword();
            return Ok(pass);
        }

        [HttpGet]
        [Route("get")]
        public ActionResult<IEnumerable<string>> Get()
        {

            var currentUser = HttpContext.User;
            DateTime TokenDate = new DateTime();

            if (currentUser.HasClaim(c => c.Type == "Date"))
            {
                TokenDate = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Date").Value);

            }

            return Ok("Custom Claims(date): " + TokenDate);

        }
    }
}
