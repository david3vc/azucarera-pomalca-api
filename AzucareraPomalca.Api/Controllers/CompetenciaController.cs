using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Competencias;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class CompetenciaController : Controller
    {
        private readonly ICompetenciaService _competenciaService;

        public CompetenciaController(ICompetenciaService competenciaService)
        {
            _competenciaService = competenciaService;
        }

        [HttpGet("PaginatedSearch")]
        public async Task<PageResponse<CompetenciaDto>> PaginatedSearch([FromQuery] PageRequest<CompetenciaFilterDto> request)
        {
            return await _competenciaService.FindAllPaginatedAsync(request);
        }
    }
}
