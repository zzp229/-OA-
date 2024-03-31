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

        // ��һ�´�mySql���ݿ��л�ȡ����
        public void test()
        {
            string connStr = "server=175.178.166.212;user=root;database=oadb;port=3306;password=h20021023;charset=utf8;SslMode=none;";

            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    string sql = "SELECT * FROM sys_dept"; // �滻YourTableNameΪ���ı���
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var tmp = reader["dept_name"].ToString();
                                //Console.WriteLine(reader["dept_name"].ToString()); // �滻ColumnNameΪ��������
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
            // �����ʹ��EFCore��
            services.AddDbContext<MyToDoContext>(option =>
            {
                var connectionString = Configuration.GetConnectionString("ToDoConnection");
                option.UseSqlite(connectionString);

            }).AddUnitOfWork<MyToDoContext>()
            .AddCustomRepository<ToDo, ToDoRepository>()
            .AddCustomRepository<Memo, MemoRepository>()
            .AddCustomRepository<User, UserRepository>()
            .AddCustomRepository<OA_api, OA_apiRepository>();  // ��������ǹ�����Ԫ

            services.AddTransient<IToDoService, ToDoService>(); // ����Ǵ�������ķ���
            services.AddTransient<IMemoService, MemoService>(); // ע�뱸��¼����
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IOA_apiService, OA_apiService>();



            // ���һ��MySql��
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
            services.AddTransient<IEmailService, EmailService>();   // �����ʼ�����


            // ����ṹ���л�ΪJSONʱ�����л��������һ������ѭ��
            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });





            var automapperConfog = new MapperConfiguration(config =>
            {
                config.AddProfile(new AutoMapperProFile());
            }); // ע��AutoMapper�ķ���

            services.AddSingleton(automapperConfog.CreateMapper()); // Mapper����

            services.AddControllers();  // Controllers����
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyToDo.Api", Version = "v1" });
            }); // Swagger����
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ������ڿ��������µ�
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
