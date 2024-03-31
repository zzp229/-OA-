using MyToDo.Api.Context.Mail;
using MyToDo.Api.Context.Mail.MailDto;
using System.Threading.Tasks;

namespace MyToDo.Api.Service.OA_Service.Mail_interface
{
    public interface IEmailService
    {
        Task<ApiResponse> MailSend(EmailDto emailDto);
    }
}
