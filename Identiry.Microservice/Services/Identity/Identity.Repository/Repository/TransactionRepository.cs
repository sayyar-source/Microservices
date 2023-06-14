using ManageMoney.Infrastructure.SystemExceptions;
using ManageMoney.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace ManageMoney.Infrastructure.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDBContext _dbContext;
        public TransactionRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Transaction> AddTransactionAsync(Transaction Transaction)
        {
            try
            {
                var result=await _dbContext.AddAsync(Transaction);
                await _dbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message);
            }
        }

        public async Task DeleteTransactionAsync(Transaction Transaction)
        {
            try
            {
                var currentTransaction=await GetTransactionByIdAsync(Transaction.Id);
                if (currentTransaction != null)
                {
                    _dbContext.Remove(currentTransaction);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message);
            }
        }

        public async Task<Transaction> GetTransactionByIdAsync(Guid id)
        {
            try
            {
                var currentTransaction=await _dbContext.Transactions.FindAsync(id);
                return currentTransaction;
            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message);
            }
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync(Guid id)
        {
            try
            {
                var TransactionList =await _dbContext.Transactions.Where(t=>t.UserId==id).ToListAsync();
                return TransactionList;
            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message);
            }
        }

        public  async Task<List<Transaction>> SearchTransactionsAsync(Guid userId, string key)
        {
            try
            {
                var transactionList =await _dbContext.Transactions.Where(x => x.Amount.ToString().Contains(key) || x.Category.Contains(key) && x.UserId==userId).ToListAsync();
        
                return transactionList;

            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message);
            }
        }
    }
}
