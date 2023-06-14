using ManageMoney.Data.Dtos;
using ManageMoney.Data.Models;
using ManageMoney.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ManageMoney.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly AppDBContext _db;
        public TokenService(IConfiguration config, AppDBContext db)
        {
            _config = config;
            _db = db;
        }
        public string BuildToken(UserLoginDto user)
        {
            try
            {
                var currentUser = Authenticate(user);
                if (currentUser == null)
                {
                    return null;
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.GivenName, currentUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString()),
                new Claim(ClaimTypes.Email, currentUser.Email),
                new Claim(ClaimTypes.GivenName, currentUser.FirstName),
                new Claim(ClaimTypes.Surname, currentUser.LastName),
                new Claim(ClaimTypes.Role, currentUser.Role)
            };

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Audience"],
                  claims,
                  expires: DateTime.Now.AddMinutes(15),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {


                Console.WriteLine("{0} In TokenService/BuildToken.", ex);
                return null;
            }
           
        }
        private User Authenticate(UserLoginDto userLogin)
        {
            try
            {
                var currentUser = _db.Users.FirstOrDefault(o => o.Email.ToLower() == userLogin.Email.ToLower() && o.Password == userLogin.Password);

                if (currentUser != null)
                {
                    return currentUser;
                }
                return null;
            }
            catch (Exception ex)
            {

                Console.WriteLine("{0} In TokenService/Authenticate.", ex);
                return null;
            }
         
        }
    }
}
