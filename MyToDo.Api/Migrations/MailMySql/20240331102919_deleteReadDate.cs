using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyToDo.Api.Migrations.MailMySql
{
    public partial class deleteReadDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadDate",
                table: "EmailRecipient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReadDate",
                table: "EmailRecipient",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
