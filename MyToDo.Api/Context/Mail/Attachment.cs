using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyToDo.Api.Context.Mail
{
    /// <summary>
    /// 附件表（Attachments）实体类
    /// </summary>
    public class Attachment
    {
        [Key]
        public int AttachmentID { get; set; }

        public int EmailID { get; set; }    // 外键指向Email

        [ForeignKey("EmailID")]
        public Email Email { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }

        public string FilePath { get; set; }
    }
}
