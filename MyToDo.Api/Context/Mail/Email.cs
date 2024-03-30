using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System;

namespace MyToDo.Api.Context.Mail
{
    public class Email
    {
        [Key]
        public int EmailID { get; set; }

        public int FromUserID { get; set; }

        [ForeignKey("FromUserID")]
        public User FromUser { get; set; }

        public string EmailBody { get; set; }

        public DateTime SentDate { get; set; }

        // 导航属性
        public List<EmailRecipient> EmailRecipients { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
