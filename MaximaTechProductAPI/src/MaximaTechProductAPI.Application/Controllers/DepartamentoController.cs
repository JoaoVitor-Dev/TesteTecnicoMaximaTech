using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaximaTechProductAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Controller funcionando!");
        }
    }
}
