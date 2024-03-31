using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using MyToDo.Api.Context;
using MyToDo.Api.Context.Mail;
using MyToDo.Api.Context.Mail.MailDto;
using MyToDo.Api.Context.Repository;
using MyToDo.Api.Extensions;
using MyToDo.Api.Service;
using MyToDo.Api.Service.OA_Service;
using MyToDo.Api.Service.OA_Service.Mail_interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //test();
        }

        // 试一下从mySql数据库中获取数据
        public void test()
        {
            string connStr = "server=175.178.166.212;user=root;database=oadb;port=3306;password=h20021023;charset=utf8;SslMode=none;";

            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    string sql = "SELECT * FROM sys_dept"; // 替换YourTableName为您的表名
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var tmp = reader["dept_name"].ToString();
                                //Console.WriteLine(reader["dept_name"].ToString()); // 替换ColumnName为您的列名
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 这个是使用EFCore的
            services.AddDbContext<MyToDoContext>(option =>
            {
                var connectionString = Configuration.GetConnectionString("ToDoConnection");
                option.UseSqlite(connectionString);

            }).AddUnitOfWork<MyToDoContext>()
            .AddCustomRepository<ToDo, ToDoRepository>()
            .AddCustomRepository<Memo, MemoRepository>()
            .AddCustomRepository<User, UserRepository>()
            .AddCustomRepository<OA_api, OA_apiRepository>();  // 后面这个是工作单元

            services.AddTransient<IToDoService, ToDoService>(); // 这个是待办事项的服务
            services.AddTransient<IMemoService, MemoService>(); // 注入备忘录服务
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IOA_apiService, OA_apiService>();



            // 添加一个MySql的
            services.AddDbContext<MailMySqlContext>(options =>
            {
                var mysqlConnectionString = Configuration.GetConnectionString("MySqlConnection");
                options.UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString));
            }).AddUnitOfWork<MailMySqlContext>()
            .AddCustomRepository<MailTest, MailTestRepository>()
            .AddCustomRepository<Email, EmailRepository>()
            .AddCustomRepository<Attachment, AttachmentRepository>()
            .AddCustomRepository<EmailRecipient, EmailRecipientRepository>();

            services.AddTransient<IMailTestService, MailTestService>();
            services.AddTransient<ISysUserService, SysUserService>();
            services.AddTransient<IEmailService, EmailService>();   // 发送邮件服务


            // 解决结构序列化为JSON时，序列化器会进入一个无限循环
            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });





            var automapperConfog = new MapperConfiguration(config =>
            {
                config.AddProfile(new AutoMapperProFile());
            }); // 注入AutoMapper的服务

            services.AddSingleton(automapperConfog.CreateMapper()); // Mapper服务

            services.AddControllers();  // Controllers服务
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyToDo.Api", Version = "v1" });
            }); // Swagger服务
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 这个是在开发环境下的
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyToDo.Api v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
            });
        }
    }
}
