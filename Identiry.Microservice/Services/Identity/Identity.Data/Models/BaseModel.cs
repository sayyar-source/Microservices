using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMoney.Data.Models
{
    public class BaseModel
    {
        [Key]
       public Guid Id { get; set; }
        public DateTime Createdate { get; set; } = DateTime.UtcNow;
       public DateTime Updatedate { get; set; } = DateTime.UtcNow;
    }
}
