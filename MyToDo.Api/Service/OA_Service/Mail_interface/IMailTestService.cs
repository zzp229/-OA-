using MyToDo.Api.Context.Mail;

namespace MyToDo.Api.Service.OA_Service.Mail_interface
{
    public interface IMailTestService : IBaseService<MailTest>
    {
    }

    /// <summary>
    /// 用户的Service接口
    /// </summary>
    public interface ISysUserService : IBaseService<SysUser> { }
}
