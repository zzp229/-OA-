using Microsoft.EntityFrameworkCore;
using MyToDo.Api.Context.Mail;

namespace MyToDo.Api.Context
{
    public class MailMySqlContext : DbContext
    {
        public MailMySqlContext(DbContextOptions<MailMySqlContext> options) : base(options)
        {

        }

        // 这两个需要空迁移的
        //public DbSet<MailTest> MailTest { get; set; }
        public DbSet<SysUser> SysUser { get; set; }

        // 下面几张是正常迁移的
        public DbSet<EmailRecipient> EmailRecipient { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<Email> Email { get; set; }


        // 测试
        //public DbSet<test> test { get; set; }
    }
}
