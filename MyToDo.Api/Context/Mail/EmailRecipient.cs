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

        public int EmailID { get; set; }    // 外键指向Email

        [ForeignKey("EmailID")]
        public Email Email { get; set; }

        public long ToUserID { get; set; }  // 外键指向User

        [ForeignKey("ToUserID")]
        public SysUser ToUser { get; set; } // 引用SysUser

        public bool IsRead { get; set; }
    }
}
