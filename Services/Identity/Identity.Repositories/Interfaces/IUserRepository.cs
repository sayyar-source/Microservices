using Services.Identity.Identity.Data.Models;

namespace Services.Identity.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> AddUser(User user);
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserByEmail(string email);
        public Task<User> UpdateUser(User user);
    }
}
