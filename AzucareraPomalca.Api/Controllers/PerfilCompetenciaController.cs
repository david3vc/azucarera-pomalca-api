using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.PerfilCompetencias;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class PerfilCompetenciaController : Controller
    {
        private readonly IPerfilCompetenciaService _perfilCompetenciaService;

        public PerfilCompetenciaController(IPerfilCompetenciaService perfilCompetenciaService)
        {
            _perfilCompetenciaService = perfilCompetenciaService;
        }

        [HttpGet("PaginatedSearch")]
        public async Task<PageResponse<PerfilCompetenciaDto>> PaginatedSearch([FromQuery] PageRequest<PerfilCompetenciaFilterDto> request)
        {
            return await _perfilCompetenciaService.FindAllPaginatedAsync(request);
        }
    }
}
