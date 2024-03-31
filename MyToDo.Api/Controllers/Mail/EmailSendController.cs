using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context.Mail;
using MyToDo.Api.Context.Mail.MailDto;
using MyToDo.Api.Service;
using MyToDo.Api.Service.OA_Service.Mail_interface;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyToDo.Api.Controllers.Mail
{



    /// <summary>
    /// 发送邮箱Controller
    /// </summary>
    [ApiController]
    //[Route("[controller]")]
    [Route("[controller]/[action]")]
    public class EmailSendController : Controller
    {
        private readonly IEmailService service;

        public EmailSendController(IEmailService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="file">附件</param>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Send([FromBody] EmailDto emailDto) => await service.MailSend(emailDto);


    }
}
