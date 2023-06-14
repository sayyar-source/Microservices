
using Microsoft.EntityFrameworkCore;
using Services.Identity.Identity.Data.Models;
using Services.Identity.Repositories.SystemExceptions;

namespace Services.Identity.Repositories
{
   public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {

        }
 
       public DbSet<User> Users { get; set; }
       public DbSet<Tokens> Tokens { get; set; }
       public DbSet<ExceptionLog> Exceptions { get; set; }
  
    }
}
