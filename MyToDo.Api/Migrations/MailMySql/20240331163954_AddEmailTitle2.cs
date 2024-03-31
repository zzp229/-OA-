using Microsoft.EntityFrameworkCore.Migrations;

namespace MyToDo.Api.Migrations.MailMySql
{
    public partial class AddEmailTitle2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "EmailTitle",
            //    table: "Email",
            //    type: "longtext",
            //    nullable: true)
            //    .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "EmailTitle",
            //    table: "Email");
        }
    }
}
