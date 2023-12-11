using AzucareraPomalca.Application.Dtos.EsfuerzoRequeridos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class EsfuerzoRequeridoController : Controller
    {
        private readonly IEsfuerzoRequeridoService _esfuerzoRequeridoService;

        public EsfuerzoRequeridoController(IEsfuerzoRequeridoService esfuerzoRequeridoService)
        {
            _esfuerzoRequeridoService = esfuerzoRequeridoService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<EsfuerzoRequeridoDto>> GetSimple()
        {
            return await _esfuerzoRequeridoService.SimpleListAsync();
        }
    }
}
