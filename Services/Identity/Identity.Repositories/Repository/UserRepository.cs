using Microsoft.EntityFrameworkCore;
using Services.Identity.Identity.Data.Models;
using Services.Identity.Repositories.Interfaces;
using Services.Identity.Repositories.SystemExceptions;

namespace Services.Identity.Repositories.Repository
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

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var result = await _dbContext.Users.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message);
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var result = await _dbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();  
                return result;
            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message);
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                var result = await _dbContext.Users.Where(x => x.Email == user.Email).FirstOrDefaultAsync();
                if(result != null)
                {
                    result.Updatedate = DateTime.UtcNow;
                    result.UserName = user.UserName;
                    result.Password = user.Password;    
                    result.FirstName = user.FirstName;
                    result.LastName = user.LastName;
                    result.UserName = user.Email;
                    //_dbContext.Users.Update(result);
                   await _dbContext.SaveChangesAsync();
                }
              
                return result;
            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message);
            }
        }
    }
}
