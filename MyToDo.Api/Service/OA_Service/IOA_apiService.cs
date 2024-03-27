using MyToDo.Shared.Dtos;
using System.Threading.Tasks;

namespace MyToDo.Api.Service.OA_Service
{
    /// <summary>
    /// api的接口中提供修
    /// 获取和修改金额
    /// 获取和修改花费
    /// 
    /// </summary>
    public interface IOA_apiService
    {
        // 修改
        Task<ApiResponse> FixMoneyAll(decimal moneyAll);

        Task<ApiResponse> FixMoneySpeet(decimal moneySpeet);

        // 获取
        Task<ApiResponse> GetMoneyAll();
    }
}
