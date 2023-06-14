using ManageMoney.Data.Models;

namespace ManageMoney.Infrastructure.Repository
{
    public interface ITransactionRepository
    {
        public Task<List<Transaction>> GetAllTransactionsAsync(Guid id);
        public Task DeleteTransactionAsync(Transaction Transaction);
        public Task<Transaction> GetTransactionByIdAsync(Guid id);
        public Task<Transaction> AddTransactionAsync(Transaction Transaction);
        public Task<List<Transaction>> SearchTransactionsAsync(Guid userId,string key);
    }
}
