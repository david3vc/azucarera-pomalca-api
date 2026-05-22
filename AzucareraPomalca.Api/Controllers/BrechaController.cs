using AzucareraPomalca.Application.Dtos.Brechas;
using AzucareraPomalca.Application.Dtos.Empleados;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class BrechaController : Controller
    {
        private readonly IBrechaService _brechaService;

        public BrechaController(IBrechaService brechaService)
        {
            _brechaService = brechaService;
        }

        // GET: api/brecha/blandas?idGerencia=...&idDivision=...
        [HttpGet("Blandas")]
        public async Task<List<BrechaBlandaDto>> Blandas([FromQuery] BrechaBlandaFilterDto filter)
        {
            return await _brechaService.BrechasBlandasAsync(filter);
        }

        // GET: api/brecha/blandas/empleados?idCompetencia=...&idGerencia=...
        [HttpGet("Blandas/Empleados")]
        public async Task<List<EmpleadoSugeridoDto>> EmpleadosBlandas([FromQuery] BrechaBlandaEmpleadosFilterDto filter)
        {
            return await _brechaService.EmpleadosConBrechaBlandaAsync(filter);
        }
    }
}
