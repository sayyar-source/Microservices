
using Services.Identity.Identity.Data.Dtos;
using Services.Identity.Identity.Data.Models;

namespace Services.Identity.Identity.Application.Service
{
    public interface IUserService
    {
        public Task<User> AddUser(UserDto userRegisterDto);
        public string GeneratePassword();
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserByEmail(string email);
        public Task<User> UpdateUser(UserDto userUpdateDto);
    }
}
