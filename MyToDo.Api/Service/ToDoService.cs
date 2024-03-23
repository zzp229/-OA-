using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyToDo.Api.Service
{



    /// <summary>
    /// 待办事项的实现
    /// ToDo服务的实现类
    /// </summary>
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper Mapper;

        // 构造函数注入工作单元
        public ToDoService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.Mapper = mapper;
        }

        public async Task<ApiResponse> AddAsync(ToDoDto model)
        {
            try
            {
                var todo = Mapper.Map<ToDo>(model); //将ToDoDto转换为ToDo（响应数据类转换为数据库相关类）
                await work.GetRepository<ToDo>().InsertAsync(todo);
                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, model);
                return new ApiResponse(false, "添加数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(todo);
                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, "");
                return new ApiResponse(false, "删除数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                var repository = work.GetRepository<ToDo>();
                var todos = await repository.GetAllAsync();
                return new ApiResponse(false, todos);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new ApiResponse(false, todo);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(ToDoDto model)
        {
            try
            {
                var dbToDo = Mapper.Map<ToDo>(model);   // ToDoDto转换为ToDo

                // 从工作单元中获取Repository存储库
                var repository = work.GetRepository<ToDo>();
                // 将要更新的数据类获取出来
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));

                // 更新
                todo.Title = dbToDo.Title;
                todo.Content = dbToDo.Content;
                todo.Status = dbToDo.Status;
                todo.UpdateDate = DateTime.Now;

                // 更新数据库
                repository.Update(todo);

                // 执行看看对不对
                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, todo); // 返回报文
                return new ApiResponse("更新数据异常!");

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

    }
}
