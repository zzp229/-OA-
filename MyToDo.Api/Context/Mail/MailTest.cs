using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyToDo.Api.Context.Mail
{
    //[Table("mail_test")]  // 这个表明存在了，只能是使用Fluent API指定表名了
    [Table("mail_test")] // 显式指定数据库中的表名
    public class MailTest
    {
        [Key] // 标记这是主键
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 标记主键值由数据库自动生成
        [Column("user_id")] // 显式指定映射到数据库列的名称
        public long UserId { get; set; }

        [Column("dept_id")] // 同样，显式指定映射到数据库列的名称
        public long? DeptId { get; set; }
    }
}
