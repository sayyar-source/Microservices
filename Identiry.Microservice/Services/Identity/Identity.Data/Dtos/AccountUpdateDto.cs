namespace ManageMoney.Data.Dtos
{
    public class AccountUpdateDto
    {
        public Guid Id { get; set; }
        public AccountType AccountType { get; set; }
        public string Name { get; set; }
    }
}
