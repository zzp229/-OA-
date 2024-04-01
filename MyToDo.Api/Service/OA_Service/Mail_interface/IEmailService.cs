using MyToDo.Api.Context.Mail;
using MyToDo.Api.Context.Mail.MailDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyToDo.Api.Service.OA_Service.Mail_interface
{
    public interface IEmailService
    {
        Task<ApiResponse> MailSend(EmailDto emailDto);

        Task<ApiResponse> GetAllMail(long ToUserID);

        Task<ApiResponse> GetMail(int EmailID);

        Task<List<MyMessage>> GetEmailsForUserAsync(long toUserId);
        Task<MyMail> GetEmailAsync(int emailId);
    }
}
