using MyToDo.Api.Context.Mail;
using MyToDo.Api.Service.OA_Service.Mail_interface;
using MyToDo.Shared.Parameters;
using System;
using System.Threading.Tasks;

namespace MyToDo.Api.Service.OA_Service
{
    public class SysUserService : ISysUserService
    {
        private readonly IUnitOfWork work;

        public SysUserService(IUnitOfWork work)
        {
            this.work = work;
        }

        public Task<ApiResponse> AddAsync(SysUser model)
        {
            return null;
        }

        public Task<ApiResponse> DeleteAsync(int id)
        {
            return null;
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter query)
        {
            try
            {
                var repository = work.GetRepository<SysUser>();
                var mailTest = await repository.GetFirstOrDefaultAsync(); // 获取第一条记录
                return new ApiResponse(true, mailTest); // 返回包含该记录的ApiResponse对象
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message); // 出现异常时返回异常信息
            }
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<SysUser>();
                var mailTest = await repository.GetFirstOrDefaultAsync(); // 获取第一条记录
                return new ApiResponse(true, mailTest); // 返回包含该记录的ApiResponse对象
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message); // 出现异常时返回异常信息
            }
        }

        public Task<ApiResponse> UpdateAsync(SysUser model)
        {
            return null;
        }
    }
}
