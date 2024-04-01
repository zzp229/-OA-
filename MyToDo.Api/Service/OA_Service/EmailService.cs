using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MyToDo.Api.Context;
using MyToDo.Api.Context.Mail;
using MyToDo.Api.Context.Mail.MailDto;
using MyToDo.Api.Service.OA_Service.Mail_interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
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


        #region 邮件的发送
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
                EmailTitle = emailDto.EmailTitle,
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
        #endregion


        #region 获取当前用户的所有邮件
        public async Task<ApiResponse> GetAllMail(long ToUserID)
        {
            // 这个不够好，要查出阅读状态 发件人信息，邮件的标题，发送时间
            try
            {
                var repository = work.GetRepository<EmailRecipient>();
                var emails = await repository.GetAllAsync(x => x.ToUserID == ToUserID);
                return new ApiResponse(true, emails);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }


        /// <summary>
        /// 获取当前用户的所有邮件
        /// </summary>
        /// <param name="toUserId"></param>
        /// <returns></returns>
        public async Task<List<MyMessage>> GetEmailsForUserAsync(long toUserId)
        {
            var connectionString = "server=175.178.166.212;port=3306;database=oadb;uid=root;pwd=h20021023;CharSet=utf8;TreatTinyAsBoolean=true;ConvertZeroDateTime=True;SslMode=None;"; // 从配置文件中获取
            var result = new List<MyMessage>();

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("GetEmailsForUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("ToUserID", toUserId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var emailInfo = new MyMessage
                            {
                                EmailID = reader.GetInt32(reader.GetOrdinal("EmailID")),
                                IsRead = reader.GetBoolean(reader.GetOrdinal("IsRead")),
                                UserName = reader.IsDBNull(reader.GetOrdinal("user_name")) ? null : reader.GetString(reader.GetOrdinal("user_name")),
                                EmailTitle = reader.IsDBNull(reader.GetOrdinal("EmailTitle")) ? null : reader.GetString(reader.GetOrdinal("EmailTitle")),
                                SentDate = reader.GetDateTime(reader.GetOrdinal("SentDate"))
                            };
                            result.Add(emailInfo);
                        }

                    }
                }
            }

            return result;
        }



        /// <summary>
        /// 根据邮箱id获取这个邮箱的信息
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public async Task<MyMail> GetEmailAsync(int emailId)
        {
            var connectionString = "server=175.178.166.212;port=3306;database=oadb;uid=root;pwd=h20021023;CharSet=utf8;TreatTinyAsBoolean=true;ConvertZeroDateTime=True;SslMode=None;"; // 使用您的实际连接字符串

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("GetEmail", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("EmailID", emailId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var myMail = new MyMail
                            {
                                UserName = reader.IsDBNull(reader.GetOrdinal("user_name")) ? null : reader.GetString(reader.GetOrdinal("user_name")),
                                EmailBody = reader.IsDBNull(reader.GetOrdinal("EmailBody")) ? null : reader.GetString(reader.GetOrdinal("EmailBody")),
                                SentDate = reader.GetDateTime(reader.GetOrdinal("SentDate")),
                                EmailTitle = reader.IsDBNull(reader.GetOrdinal("EmailTitle")) ? null : reader.GetString(reader.GetOrdinal("EmailTitle")),
                                FileName = reader.IsDBNull(reader.GetOrdinal("FileName")) ? null : reader.GetString(reader.GetOrdinal("FileName")),
                            };
                            return myMail;
                        }
                    }
                }
            }

            return null; // 如果没有数据返回，则返回null
        }





        #endregion



        #region 获取邮件信息
        public async Task<ApiResponse> GetMail(int EmailID)
        {
            try
            {
                var repository = work.GetRepository<Email>();
                var email = await repository.GetAllAsync(x => x.EmailID == EmailID);
                return new ApiResponse(true, email);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
        #endregion




        #region 点击后这个邮件变为已读
        public async Task<ApiResponse> FixReaded(int emailId)
        {
            try
            {
                var repository = work.GetRepository<EmailRecipient>();
                var emailRecipients = await repository.GetAllAsync(x => x.EmailID == emailId);
                var emailRecipient = emailRecipients.FirstOrDefault();

                if (emailRecipient != null)
                {
                    emailRecipient.IsRead = true; // 标记为已读

                    // 直接使用DbContext的Update方法标记实体为已修改
                    work.Update(emailRecipient);

                    var result = await work.SaveChangesAsync();
                    if (result > 0)
                    {
                        // 数据库成功更新
                        return new ApiResponse(true, "邮件已标记为已读");
                    }
                    else
                    {
                        // 未做任何更新
                        return new ApiResponse(false, "更新失败，可能邮件已被标记为已读。");
                    }
                }
                else
                {
                    return new ApiResponse(false, "未找到指定的邮件");
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, ex.Message);
            }
        }


        #endregion

    }


}
