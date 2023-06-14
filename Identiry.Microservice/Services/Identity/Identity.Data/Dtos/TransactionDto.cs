using ManageMoney.Data.Dtos;

namespace ManageMoney.Data.Dtos
{
    public class TransactionDto
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
    }
}
