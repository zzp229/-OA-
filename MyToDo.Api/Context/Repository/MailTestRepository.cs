using Microsoft.EntityFrameworkCore;
using MyToDo.Api.Context.Mail;
using MyToDo.Api.Context.Mail.MailDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyToDo.Api.Context.Repository
{
    /// <summary>
    /// 让这些用上UnitOfWork
    /// </summary>
    public class MailTestRepository : Repository<MailTest>, IRepository<MailTest>
    {
        public MailTestRepository(MailMySqlContext dbContext) : base(dbContext)
        {

        }
    }

    public class SysUserRepository : Repository<SysUser>, IRepository<SysUser>
    {
        public SysUserRepository(MailMySqlContext dbContext) : base(dbContext)
        {

        }
    }

    public class EmailRepository : Repository<Email>, IRepository<Email>
    {
        public EmailRepository(MailMySqlContext dbContext) : base(dbContext)
        {

        }
    }

    public class AttachmentRepository : Repository<Attachment>, IRepository<Attachment>
    {
        public AttachmentRepository(MailMySqlContext dbContext) : base(dbContext)
        {

        }
    }

    public class EmailRecipientRepository : Repository<EmailRecipient>, IRepository<EmailRecipient>
    {
        public EmailRecipientRepository(MailMySqlContext dbContext) : base(dbContext)
        {

        }
    }

}
