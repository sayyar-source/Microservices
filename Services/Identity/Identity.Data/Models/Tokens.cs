
namespace Services.Identity.Identity.Data.Models
{
   public class Tokens:BaseModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
