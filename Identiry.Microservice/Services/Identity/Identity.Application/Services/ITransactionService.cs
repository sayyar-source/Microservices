

using ManageMoney.Data.Dtos;
using ManageMoney.Data.Models;

namespace ManageMoney.Application.Services
{
    public interface ITransactionService
    {
        public Task<List<Transaction>> GetAllTransactionsAsync(Guid id);
        public Task DeleteTransactionAsync(Guid id);
        public Task<Transaction> GetTransactionByIdAsync(Guid id);
        public Task<Transaction> AddTransactionAsync(TransactionDto TransactionDto);
        public Task<List<Transaction>> SearchTransactionsAsync(Guid userId,string key);
    }
}
