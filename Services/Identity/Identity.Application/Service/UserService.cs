using Mapster;
using Services.Identity.Identity.Data.Dtos;
using Services.Identity.Identity.Data.Models;
using Services.Identity.Repositories.Interfaces;
using System.Security.Cryptography;


namespace Services.Identity.Identity.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> AddUser(UserDto userRegisterDto)
        {
            var user= userRegisterDto.Adapt<User>();
            user.UserName = user.Email;
            user.Role = "standart";
            return await _userRepository.AddUser(user);
        }
        public string GeneratePassword()
        {
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numberChars = "0123456789";
            const string symbolChars = "!@#$%^&*()_-+=[{]}\\|;:',<.>/?";
            const string allChars = lowercaseChars + uppercaseChars + numberChars + symbolChars;
            byte[] randomBytes = new byte[12];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            char[] chars = new char[12];
            for (int i = 0; i < 12; i++)
            {
                int index = randomBytes[i] % allChars.Length;
                chars[i] = allChars[index];
            }
            return new string(chars);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<User> UpdateUser(UserDto userUpdateDto)
        {
            var user = userUpdateDto.Adapt<User>();
            return await _userRepository.UpdateUser(user);
        }
    }
}
