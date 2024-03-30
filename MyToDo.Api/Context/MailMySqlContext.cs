using Microsoft.EntityFrameworkCore;
using MyToDo.Api.Context.Mail;

namespace MyToDo.Api.Context
{
    public class MailMySqlContext : DbContext
    {
        public MailMySqlContext(DbContextOptions<MailMySqlContext> options) : base(options)
        {

        }


        public DbSet<MailTest> MailTest { get; set; }
        public DbSet<SysUser> SysUser { get; set; }
    }
}
