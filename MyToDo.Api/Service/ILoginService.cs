using MyToDo.Shared.Dtos;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace MyToDo.Api.Service
{
    public interface ILoginService
    {
        Task<ApiResponse> LoginAsync(string Account, string Password);

        Task<ApiResponse> Resgiter(UserDto user);
    }
}
