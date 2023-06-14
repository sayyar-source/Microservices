
namespace ManageMoney.Data.Dtos
{
    public class AccountDto
    {
        public Guid UserId { get; set; }
        public AccountType AccountType { get; set; }
        public string Name { get; set; }
    }
}
