using ManageMoney.Data.Dtos;
using ManageMoney.Data.Models;
using ManageMoney.Infrastructure.Repository;
using Mapster;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ManageMoney.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> AddUser(UserRegisterDto userRegisterDto)
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
    }
}
