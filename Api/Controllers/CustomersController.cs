using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Services;

namespace Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService customersService;

        public CustomersController(ICustomersService customersService)
        {
            this.customersService = customersService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await customersService.GetCustomersAsync();
            return Ok(model);
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS");
            return Ok();
        }
    }
}
