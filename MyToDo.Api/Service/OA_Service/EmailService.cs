using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context.Mail;
using MyToDo.Api.Context.Mail.MailDto;
using MyToDo.Api.Service.OA_Service.Mail_interface;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyToDo.Api.Service.OA_Service
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public EmailService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }

        /// <summary>
        /// 自增字段留空它会自己加上去的，能有多少填多少
        /// </summary>
        /// <param name="emailDto"></param>
        /// <returns></returns>
        public async Task<ApiResponse> MailSend(EmailDto emailDto)
        {
            #region 处理邮件文本
            // 这里可以写成Mapper，但是一直报我的错
            Email email = new Email()
            {
                SentDate = DateTime.Now,
                FromUserID = emailDto.FromUserID,
                EmailBody = emailDto.EmailBody,
            };

            try
            {
                await work.GetRepository<Email>().InsertAsync(email);
                if (await work.SaveChangesAsync() > 0)
                {
                    int EmailID = email.EmailID;    // 插入完成后返回的EmailId
                    await MailRecepient(EmailID, emailDto.ToUserID);

                    if (!string.IsNullOrEmpty(emailDto.FileName))    // 有附加文件才调用这个
                        await MailAttach(EmailID, emailDto.FileName);

                    return new ApiResponse(true, email);
                }

                return new ApiResponse(false, "添加数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
            #endregion

            return null;
        }

        /// <summary>
        /// 邮件发送给一个人的
        /// </summary>
        /// <param name="emailId"></param>
        /// <param name="ToUserID"></param>
        /// <returns></returns>
        public async Task MailRecepient(int emailId, long ToUserID)
        {
            try
            {
                EmailRecipient emailRecipient = new EmailRecipient()
                {
                    EmailID = emailId, // 这里应该使用方法参数
                    ToUserID = ToUserID, // 同上，使用方法参数
                    IsRead = false,
                };

                // 确保InsertAsync方法调用时传入了emailRecipient实体对象
                await work.GetRepository<EmailRecipient>().InsertAsync(emailRecipient);
                if (await work.SaveChangesAsync() > 0)
                {
                    // 插入成功
                }
                else
                {
                    // 插入失败的处理
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 邮件附件
        /// </summary>
        /// <param name="emailId">邮箱ID</param>
        /// <param name="FileType">文件类型</param>
        /// <param name="FilePath">文件路径</param>
        /// <returns></returns>
        public async Task MailAttach(int emailId, string fileName)
        {
            // 创建要插入的对象
            Attachment attachment = new Attachment()
            {
                EmailID = emailId,
                FileName = fileName,
            };

            try
            {
                await work.GetRepository<Attachment>().InsertAsync(attachment);
                if (await work.SaveChangesAsync() > 0)
                {
                    // 插入成功了
                }
                else
                {
                    // 插入失败
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
