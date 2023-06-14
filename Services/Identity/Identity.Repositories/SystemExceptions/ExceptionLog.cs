using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Identity.Repositories.SystemExceptions
{
    public class ExceptionLog
    {
        [Key]
        public string EventId { get; set; }
        public DateTime Timestamp { get; set; }
        public string QueryParameters { get; set; }
        public string BodyParameters { get; set; }
        public string StackTrace { get; set; }
    }
}
