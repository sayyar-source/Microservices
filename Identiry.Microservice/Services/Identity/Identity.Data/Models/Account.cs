using ManageMoney.Data.Dtos;
namespace ManageMoney.Data.Models
{
    public class Account:BaseModel
    {
        public Guid UserId { get; set; }
        public AccountType AccountType { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }  
    }
}
