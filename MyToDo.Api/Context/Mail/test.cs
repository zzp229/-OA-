using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyToDo.Api.Context.Mail
{
    [Table("ATest")]
    public class test
    {
        [Key]
        public int EmailID { get; set; }

        public long FromUserID { get; set; }
    }
}
