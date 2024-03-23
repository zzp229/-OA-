using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Api.Service;
using System.Threading.Tasks;

namespace MyToDo.Api.Controllers
{
    /// <summary>
    /// 待办事项控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService service;

        public ToDoController(IToDoService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 下面直接调用Service中的工作单元操作数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await service.GetSingleAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll(int id) => await service.GetAllAsync();

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] ToDo model) => await service.AddAsync(model);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] ToDo model) => await service.UpdateAsync(model);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await service.DeleteAsync(id);


    }

}
