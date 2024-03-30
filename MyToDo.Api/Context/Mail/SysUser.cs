using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MyToDo.Api.Context.Mail
{
    [Table("sys_user")]
    public class SysUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public long UserId { get; set; }

        [Column("dept_id")]
        public long? DeptId { get; set; }

        [Required]
        [Column("user_name")]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required]
        [Column("nick_name")]
        [StringLength(30)]
        public string NickName { get; set; }

        [Column("user_type", TypeName = "varchar(2)")]
        [StringLength(2)]
        public string UserType { get; set; } = "00";

        [Column("email", TypeName = "varchar(50)")]
        [StringLength(50)]
        public string Email { get; set; } = "";

        [Column("phonenumber", TypeName = "varchar(11)")]
        [StringLength(11)]
        public string PhoneNumber { get; set; } = "";

        [Column("sex", TypeName = "char(1)")]
        [StringLength(1)]
        public string Sex { get; set; } = "0";

        [Column("avatar", TypeName = "varchar(100)")]
        [StringLength(100)]
        public string Avatar { get; set; } = "";

        [Required]
        [Column("password", TypeName = "varchar(100)")]
        [StringLength(100)]
        public string Password { get; set; } = "";

        [Column("status", TypeName = "char(1)")]
        [StringLength(1)]
        public string Status { get; set; } = "0";

        [Column("del_flag", TypeName = "char(1)")]
        [StringLength(1)]
        public string DelFlag { get; set; } = "0";

        [Column("login_ip", TypeName = "varchar(128)")]
        [StringLength(128)]
        public string LoginIp { get; set; } = "";

        [Column("login_date")]
        public DateTime? LoginDate { get; set; }

        [Column("create_by", TypeName = "varchar(64)")]
        [StringLength(64)]
        public string CreateBy { get; set; } = "";

        [Column("create_time")]
        public DateTime? CreateTime { get; set; }

        [Column("update_by", TypeName = "varchar(64)")]
        [StringLength(64)]
        public string UpdateBy { get; set; } = "";

        [Column("update_time")]
        public DateTime? UpdateTime { get; set; }

        [Column("remark", TypeName = "varchar(500)")]
        [StringLength(500)]
        public string Remark { get; set; }
    }
}
