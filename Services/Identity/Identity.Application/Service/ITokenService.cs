

using Services.Identity.Identity.Data.Dtos;

namespace Services.Identity.Identity.Application.Service
{
    public interface ITokenService
    {
       public string BuildToken(UserLoginDto user);
    }
}
