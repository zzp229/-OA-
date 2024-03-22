using System.Threading.Tasks;

namespace MyToDo.Api.Service
{
    /// <summary>
    /// 增删改查接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T>
    {
        Task<ApiResponse> GetAllAsync();

        Task<ApiResponse> GetSingleAsync(int id);

        Task<ApiResponse> AddAsync(T model);

        Task<ApiResponse> UpdateAsync(T model);

        Task<ApiResponse> DeleteAsync(int id);
    }
}
