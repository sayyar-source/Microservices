using ManageMoney.Data.Dtos;

namespace ManageMoney.Api.ViewModels.Requests
{
    public class TransactionRequest
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
    }
}
