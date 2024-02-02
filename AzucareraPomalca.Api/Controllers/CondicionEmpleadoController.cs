using AzucareraPomalca.Application.Dtos.CondicionEmpleados;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class CondicionEmpleadoController : Controller
    {
        private readonly ICondicionEmpleadoService _condicionEmpleadoService;

        public CondicionEmpleadoController(ICondicionEmpleadoService condicionEmpleadoService)
        {
            _condicionEmpleadoService = condicionEmpleadoService;
        }

        // GET: api/values
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CondicionEmpleadoDto>))]
        public async Task<IEnumerable<CondicionEmpleadoDto>> Get()
        {
            return await _condicionEmpleadoService.SimpleListAsync();
        }
    }
}
