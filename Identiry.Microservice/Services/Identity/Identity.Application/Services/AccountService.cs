using ManageMoney.Infrastructure.Repository;
using ManageMoney.Data.Dtos;
using ManageMoney.Data.Models;
using Mapster;


namespace ManageMoney.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Account> AddAccountAsync(AccountDto accountDto)
        {
          var account = accountDto.Adapt<Account>();
          return await _accountRepository.AddAccountAsync(account);
        }

        public async Task DeleteAccountAsync(Guid id)
        {
          
            var currentAccount =await GetAccountByIdAsync(id);
            if (currentAccount != null)
            {
               await _accountRepository.DeleteAccountAsync(currentAccount);
            }
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            return await _accountRepository.GetAccountByIdAsync(id);
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
           return await _accountRepository.GetAllAccountsAsync();
        }

        public async Task UpdateAccountAsync(AccountUpdateDto accountUpdate)
        {
            var currentAccount =await  GetAccountByIdAsync(accountUpdate.Id);

            if (currentAccount != null)
            {
                currentAccount.Updatedate = DateTime.UtcNow;
                currentAccount.Name = accountUpdate.Name;
                currentAccount.AccountType = accountUpdate.AccountType;
                await  _accountRepository.UpdateAccountAsync(currentAccount);
            }
        }      
    }
}
