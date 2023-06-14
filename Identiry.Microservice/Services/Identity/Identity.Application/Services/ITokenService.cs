

using ManageMoney.Data.Dtos;

namespace ManageMoney.Application.Services
{
    public interface ITokenService
    {
       public string BuildToken(UserLoginDto user);
    }
}
