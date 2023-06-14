using ManageMoney.Data.Dtos;
using NpgsqlTypes;

namespace ManageMoney.Data.Models
{
    public class Transaction:BaseModel
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public User User { get; set; }  
        public Account Account { get; set; }
      
    }
}
