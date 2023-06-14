using ManageMoney.Infrastructure.SystemExceptions;
using ManageMoney.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace ManageMoney.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDBContext _dbContext;
        public AccountRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Account> AddAccountAsync(Account account)
        {
            try
            {
                var result=await _dbContext.AddAsync(account);
                await _dbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message);
            }
        }

        public async Task DeleteAccountAsync(Account account)
        {
            try
            {
                var currentAccount=await GetAccountByIdAsync(account.Id);
                if (currentAccount != null)
                {
                    _dbContext.Remove(currentAccount);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message);
            }
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            try
            {
                var currentAccount=await _dbContext.Accounts.FindAsync(id);
                return currentAccount;
            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message);
            }
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            try
            {
                var accountList =await _dbContext.Accounts.ToListAsync();
                return accountList;
            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message);
            }
        }

        public async Task UpdateAccountAsync(Account account)
        {
            try
            {
                _dbContext.Accounts.Update(account);
                await _dbContext.SaveChangesAsync(); 
            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message);
            }
        }
    }
}
