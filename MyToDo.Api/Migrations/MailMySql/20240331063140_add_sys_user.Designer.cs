﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyToDo.Api.Context;

namespace MyToDo.Api.Migrations.MailMySql
{
    [DbContext(typeof(MailMySqlContext))]
    [Migration("20240331063140_add_sys_user")]
    partial class add_sys_user
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");
#pragma warning restore 612, 618
        }
    }
}
