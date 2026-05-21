using AzucareraPomalca.Application.Dtos.Equivalencias;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class EquivalenciasController : Controller
    {
        private readonly IEquivalenciaService _service;

        public EquivalenciasController(IEquivalenciaService service)
        {
            _service = service;
        }

        [HttpPost("Regularizar")]
        public async Task<RegularizarResultDto> Regularizar()
        {
            return await _service.RegularizarAsync();
        }
    }
}
