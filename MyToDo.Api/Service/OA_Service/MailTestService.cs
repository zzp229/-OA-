using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Api.Context.Mail;
using MyToDo.Api.Service.OA_Service.Mail_interface;
using MyToDo.Shared.Parameters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Service.OA_Service
{
    public class MailTestService : IMailTestService
    {
        private readonly IUnitOfWork work;

        public MailTestService(IUnitOfWork work)
        {
            this.work = work;
        }

        public async Task<ApiResponse> AddAsync(MailTest model)
        {
            try
            {
                await work.GetRepository<MailTest>().InsertAsync(model);
                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, model); /// 这里的todo写成了model导致没有返回正确的添加数据
                return new ApiResponse(false, "添加数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public Task<ApiResponse> DeleteAsync(int id)
        {
            return null;
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var repository = work.GetRepository<MailTest>();
                var mailTest = await repository.GetFirstOrDefaultAsync(); // 获取第一条记录
                return new ApiResponse(true, mailTest); // 返回包含该记录的ApiResponse对象
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message); // 出现异常时返回异常信息
            }
        }

        public Task<ApiResponse> GetSingleAsync(int id)
        {
            return null;
        }

        public Task<ApiResponse> UpdateAsync(MailTest model)
        {
            return null;
        }
    }
}
