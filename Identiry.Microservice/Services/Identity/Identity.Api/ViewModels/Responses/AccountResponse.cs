using ManageMoney.Data.Dtos;

namespace ManageMoney.Api.ViewModels.Responses
{
    public class AccountResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public AccountType AccountType { get; set; }
        public string Name { get; set; }
    }
}
