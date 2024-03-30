using MyToDo.Api.Context.Mail;

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
}
