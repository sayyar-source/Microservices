
using System.ComponentModel.DataAnnotations;

namespace Services.Identity.Identity.Data.Models
{
    public class BaseModel
    {
        [Key]
       public Guid Id { get; set; }
        public DateTime Createdate { get; set; } = DateTime.UtcNow;
       public DateTime Updatedate { get; set; } = DateTime.UtcNow;
    }
}
