

using ManageMoney.Data.Dtos;
using ManageMoney.Data.Models;

namespace ManageMoney.Application.Services
{
    public interface IAccountService
    {
        public Task<List<Account>> GetAllAccountsAsync();
        public Task UpdateAccountAsync(AccountUpdateDto accountUpdate);
        public Task DeleteAccountAsync(Guid id);
        public Task<Account> GetAccountByIdAsync(Guid id);
        public Task<Account> AddAccountAsync(AccountDto accountDto);
    }
}
