using ManageMoney.Data.Dtos;

namespace ManageMoney.Data.Models
{
    public class TransactionUpdateDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
    }
}
