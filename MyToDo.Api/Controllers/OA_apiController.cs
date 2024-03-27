using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Service;
using MyToDo.Api.Service.OA_Service;
using System.Threading.Tasks;

namespace MyToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OA_apiController : Controller
    {
        private readonly IOA_apiService service;
        public OA_apiController(IOA_apiService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ApiResponse> GetMoneyAll() => await service.GetMoneyAll();

        [HttpGet]
        public async Task<ApiResponse> FixMoneyAll(decimal moneyAll) => await service.FixMoneyAll(moneyAll);

        [HttpGet]
        public async Task<ApiResponse> FixMoneySpeet(decimal moneySpeet) => await service.FixMoneySpeet(moneySpeet);
    }
}
