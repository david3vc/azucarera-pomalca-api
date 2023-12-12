using AzucareraPomalca.Application.Dtos.TipoCompetencias;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class TipoCompetenciaController : Controller
    {
        private readonly ITipoCompetenciaService _tipoCompetenciaService;

        public TipoCompetenciaController(ITipoCompetenciaService tipoCompetenciaService)
        {
            _tipoCompetenciaService = tipoCompetenciaService;
        }

        // GET: api/tipocompetencias
        [HttpGet]
        public async Task<IEnumerable<TipoCompetenciaDto>> GetSimple()
        {
            return await _tipoCompetenciaService.SimpleListAsync();
        }
    }
}
