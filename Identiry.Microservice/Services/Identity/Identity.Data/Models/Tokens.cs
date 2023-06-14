using ManageMoney.Data.Models;

namespace ManageMoney.Data.Models
{
   public class Tokens:BaseModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
