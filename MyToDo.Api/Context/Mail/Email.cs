using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System;

namespace MyToDo.Api.Context.Mail
{
    /// <summary>
    /// 邮件表（Emails）
    /// </summary>
    [Table("Email")]
    public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 明确指定EmailID为自增
        public int EmailID { get; set; }

        public string EmailTitle { get; set; }  // 邮箱标题
        public long FromUserID { get; set; }    // 这个到时候自己获取就行了，不搞外键了

        [ForeignKey("FromUserID")]
        public SysUser FromUser { get; set; } // 引用SysUser

        public string EmailBody { get; set; }

        public DateTime SentDate { get; set; }

        // 导航属性
        public List<EmailRecipient> EmailRecipients { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
