using ManageMoney.Infrastructure.Repository;
using ManageMoney.Data.Dtos;
using ManageMoney.Data.Models;
using Mapster;


namespace ManageMoney.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Transaction> AddTransactionAsync(TransactionDto transactionDto)
        {
          var transaction = transactionDto.Adapt<Transaction>();
          transaction.TransactionDate = DateTime.UtcNow;
          return await _transactionRepository.AddTransactionAsync(transaction);
        }

        public async Task DeleteTransactionAsync(Guid id)
        {
          
            var currentTransaction =await GetTransactionByIdAsync(id);
            if (currentTransaction != null)
            {
               await _transactionRepository.DeleteTransactionAsync(currentTransaction);
            }
        }

        public async Task<Transaction> GetTransactionByIdAsync(Guid id)
        {
            return await _transactionRepository.GetTransactionByIdAsync(id);
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync(Guid id)
        {
           return await _transactionRepository.GetAllTransactionsAsync(id);
        }

        public Task<List<Transaction>> SearchTransactionsAsync(Guid userId,string key)
        {
            return _transactionRepository.SearchTransactionsAsync(userId,key);
        }
    }
}
