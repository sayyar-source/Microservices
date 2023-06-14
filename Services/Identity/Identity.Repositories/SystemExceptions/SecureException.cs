using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Identity.Repositories.SystemExceptions
{
    public class SecureException : Exception
    {
        public SecureException(string message) : base(message)
        {
        }
    }

}
