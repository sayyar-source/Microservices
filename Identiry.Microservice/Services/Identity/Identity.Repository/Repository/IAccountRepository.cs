using ManageMoney.Data.Models;

namespace ManageMoney.Infrastructure.Repository
{
    public interface IAccountRepository
    {
        public Task<List<Account>> GetAllAccountsAsync();
        public Task UpdateAccountAsync(Account account);
        public Task DeleteAccountAsync(Account account);
        public Task<Account> GetAccountByIdAsync(Guid id);
        public Task<Account> AddAccountAsync(Account account);
    }
}
