﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyToDo.Api.Context;

namespace MyToDo.Api.Migrations.MailMySql
{
    [DbContext(typeof(MailMySqlContext))]
    partial class MailMySqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("MyToDo.Api.Context.Mail.Attachment", b =>
                {
                    b.Property<int>("AttachmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EmailID")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasColumnType("longtext");

                    b.HasKey("AttachmentID");

                    b.HasIndex("EmailID");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("MyToDo.Api.Context.Mail.Email", b =>
                {
                    b.Property<int>("EmailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("EmailBody")
                        .HasColumnType("longtext");

                    b.Property<long>("FromUserID")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("EmailID");

                    b.HasIndex("FromUserID");

                    b.ToTable("Email");
                });

            modelBuilder.Entity("MyToDo.Api.Context.Mail.EmailRecipient", b =>
                {
                    b.Property<int>("RecipientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EmailID")
                        .HasColumnType("int");

                    b.Property<bool>("IsRead")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("ToUserID")
                        .HasColumnType("bigint");

                    b.HasKey("RecipientID");

                    b.HasIndex("EmailID");

                    b.HasIndex("ToUserID");

                    b.ToTable("EmailRecipient");
                });

            modelBuilder.Entity("MyToDo.Api.Context.Mail.SysUser", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<string>("Avatar")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("avatar");

                    b.Property<string>("CreateBy")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("create_by");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time");

                    b.Property<string>("DelFlag")
                        .HasMaxLength(1)
                        .HasColumnType("char(1)")
                        .HasColumnName("del_flag");

                    b.Property<long?>("DeptId")
                        .HasColumnType("bigint")
                        .HasColumnName("dept_id");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("email");

                    b.Property<DateTime?>("LoginDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("login_date");

                    b.Property<string>("LoginIp")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("login_ip");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("nick_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)")
                        .HasColumnName("phonenumber");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("remark");

                    b.Property<string>("Sex")
                        .HasMaxLength(1)
                        .HasColumnType("char(1)")
                        .HasColumnName("sex");

                    b.Property<string>("Status")
                        .HasMaxLength(1)
                        .HasColumnType("char(1)")
                        .HasColumnName("status");

                    b.Property<string>("UpdateBy")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("update_by");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_time");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("user_name");

                    b.Property<string>("UserType")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("user_type");

                    b.HasKey("UserId");

                    b.ToTable("sys_user");
                });

            modelBuilder.Entity("MyToDo.Api.Context.Mail.Attachment", b =>
                {
                    b.HasOne("MyToDo.Api.Context.Mail.Email", "Email")
                        .WithMany("Attachments")
                        .HasForeignKey("EmailID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Email");
                });

            modelBuilder.Entity("MyToDo.Api.Context.Mail.Email", b =>
                {
                    b.HasOne("MyToDo.Api.Context.Mail.SysUser", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromUser");
                });

            modelBuilder.Entity("MyToDo.Api.Context.Mail.EmailRecipient", b =>
                {
                    b.HasOne("MyToDo.Api.Context.Mail.Email", "Email")
                        .WithMany("EmailRecipients")
                        .HasForeignKey("EmailID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyToDo.Api.Context.Mail.SysUser", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Email");

                    b.Navigation("ToUser");
                });

            modelBuilder.Entity("MyToDo.Api.Context.Mail.Email", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("EmailRecipients");
                });
#pragma warning restore 612, 618
        }
    }
}
