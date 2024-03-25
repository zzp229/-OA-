using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System.Threading.Tasks;

namespace MyToDo.Api.Service
{
    public interface IToDoService : IBaseService<ToDoDto>
    {
        Task<ApiResponse> GetAllAsync(ToDoParameter query);    // 给的这个参数是分页查询的参数

        Task<ApiResponse> Summary();
    }
}
