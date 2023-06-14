using ManageMoney.Data.Models;
using ManageMoney.Infrastructure.SystemExceptions;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ManageMoney.Infrastructure
{
   public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {

        }
 
       public DbSet<User> Users { get; set; }
       public DbSet<Tokens> Tokens { get; set; }
       public DbSet<ExceptionLog> Exceptions { get; set; }
       public DbSet<Account> Accounts { get; set; }
       public DbSet<Transaction> Transactions { get; set; }
    }
}
