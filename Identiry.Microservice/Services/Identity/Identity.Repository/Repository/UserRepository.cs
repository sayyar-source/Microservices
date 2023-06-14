using ManageMoney.Data.Models;
using ManageMoney.Infrastructure.SystemExceptions;

namespace ManageMoney.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
		private readonly AppDBContext _dbContext;
        public UserRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;       
        }
        public async Task<User> AddUser(User user)
        {
			try
			{
                var result =await _dbContext.Users.AddAsync(user);
                _dbContext.SaveChanges();
                return result.Entity;
			}
			catch (Exception ex)
			{
				throw new SecureException(ex.Message);
			}
            
        }
    }
}
