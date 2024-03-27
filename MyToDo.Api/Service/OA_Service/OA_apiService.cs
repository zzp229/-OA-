using System;
using System.Threading.Tasks;
using MyToDo.Api.Context;

namespace MyToDo.Api.Service.OA_Service
{
    public class OA_apiService : IOA_apiService
    {
        private readonly IUnitOfWork work;

        public OA_apiService(IUnitOfWork work)
        {
            this.work = work;
        }

        public async Task<ApiResponse> FixMoneyAll(decimal moneyAll)
        {
            try
            {
                // 工作单元中获取Repository数据库
                var repository = work.GetRepository<OA_api>();
                // 对象获取出来
                var oa_Api = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == 1);    // 只有第一行存储数据

                // 更新
                oa_Api.All = moneyAll;

                // 更新数据库
                repository.Update(oa_Api);

                // 执行更新
                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, oa_Api);
                return new ApiResponse("更新数据异常!");

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> FixMoneySpeet(decimal moneySpeet)
        {
            try
            {
                // 工作单元中获取Repository数据库
                var repository = work.GetRepository<OA_api>();
                // 对象获取出来
                var oa_Api = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == 1);    // 只有第一行存储数据

                // 更新
                oa_Api.speet = moneySpeet;

                // 更新数据库
                repository.Update(oa_Api);

                // 执行更新
                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, oa_Api);
                return new ApiResponse("更新数据异常!");

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        /// <summary>
        /// 返回一个对象，包含总金额和已花费
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse> GetMoneyAll()
        {
            try
            {
                var repository = work.GetRepository<OA_api>();
                var oa_Api = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == 1);    // 只有第一行存储数据
                return new ApiResponse(true, oa_Api);   // 一下子返回了整个对象
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

    }
}
