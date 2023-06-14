
using ManageMoney.Data.Models;
using System.Threading.Tasks;

namespace ManageMoney.Infrastructure.Repository
{
    public interface IUserRepository
    {
        public Task<User> AddUser(User user);
    }
}
