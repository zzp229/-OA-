using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyToDo.Api.Migrations.MailMySql
{
    public partial class add_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "sys_user",
            //    columns: table => new
            //    {
            //        user_id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        dept_id = table.Column<long>(type: "bigint", nullable: true),
            //        user_name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        nick_name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        user_type = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        phonenumber = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        sex = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        avatar = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        del_flag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        login_ip = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        login_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
            //        create_by = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        create_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
            //        update_by = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        update_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
            //        remark = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_sys_user", x => x.user_id);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "sys_user");
        }
    }
}
