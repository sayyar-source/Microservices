

using ManageMoney.Data.Dtos;

namespace ManageMoney.Api.ViewModels.Requests
{
    public class AccountRequest
    {
        public AccountType AccountType { get; set; }
        public string Name { get; set; }
    }
}
