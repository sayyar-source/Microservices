
using ManageMoney.Data.Dtos;
using ManageMoney.Data.Models;

namespace ManageMoney.Application.Services
{
    public interface IUserService
    {
        public Task<User> AddUser(UserRegisterDto userRegisterDto);
        public string GeneratePassword();
    }
}
