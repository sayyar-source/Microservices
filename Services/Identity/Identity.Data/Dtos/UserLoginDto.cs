
using System.ComponentModel.DataAnnotations;


namespace Services.Identity.Identity.Data.Dtos
{
   public class UserLoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
    }
}
