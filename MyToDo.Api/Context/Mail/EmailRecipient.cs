using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MyToDo.Api.Context.Mail
{
    /// <summary>
    /// 邮件收件人表（EmailRecipients）实体类
    /// </summary>
    public class EmailRecipient
    {
        [Key]
        public int RecipientID { get; set; }

        public int EmailID { get; set; }

        [ForeignKey("EmailID")]
        public Email Email { get; set; }

        public int ToUserID { get; set; }

        [ForeignKey("ToUserID")]
        public User ToUser { get; set; }

        public bool IsRead { get; set; }

        public DateTime? ReadDate { get; set; }
    }
}
