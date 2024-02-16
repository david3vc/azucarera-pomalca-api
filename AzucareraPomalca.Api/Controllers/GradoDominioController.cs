using AzucareraPomalca.Application.Dtos.GradoDominios;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class GradoDominioController : Controller
    {
        private readonly IGradoDominioService _gradoDominioService;

        public GradoDominioController(IGradoDominioService gradoDominioService)
        {
            _gradoDominioService = gradoDominioService;
        }

        [HttpGet("FindByNivelAndIdCompetencia")]
        public async Task<GradoDominioDto> PaginatedSearch([FromQuery] GradoDominioFilter request)
        {
            return await _gradoDominioService.FindByNivelAndIdCompetenciaAsync(request);
        }
    }
}
