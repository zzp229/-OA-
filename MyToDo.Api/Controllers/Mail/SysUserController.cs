using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Service;
using MyToDo.Api.Service.OA_Service.Mail_interface;
using MyToDo.Shared.Parameters;
using System.Threading.Tasks;

namespace MyToDo.Api.Controllers.Mail
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SysUserController : ControllerBase
    {
        private readonly ISysUserService service;

        public SysUserController(ISysUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter parameter) => await service.GetAllAsync(parameter);
    }
}
