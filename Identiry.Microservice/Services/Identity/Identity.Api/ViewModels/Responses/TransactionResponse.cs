using ManageMoney.Data.Dtos;

namespace ManageMoney.Api.ViewModels.Responses
{
    public class TransactionResponse
    { 
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
    }
}
