using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context.Mail;
using MyToDo.Api.Service;
using MyToDo.Api.Service.OA_Service.Mail_interface;
using MyToDo.Shared.Parameters;
using System.Threading.Tasks;

namespace MyToDo.Api.Controllers.Mail
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MailTestController : ControllerBase
    {
        private readonly IMailTestService service;

        public MailTestController(IMailTestService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] MailTest mailTest) => await service.AddAsync(mailTest);

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter parameter) => await service.GetAllAsync(parameter);
    }
}
