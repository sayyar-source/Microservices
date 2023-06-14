
namespace ManageMoney.Data.Models
{
   public class User:BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
        public ICollection<Account> Account { get; set; }
    }
}
