using AzucareraPomalca.Application.Dtos.Niveles;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class NivelController : Controller
    {
        private readonly INivelService _nivelService;

        public NivelController(INivelService nivelService)
        {
            _nivelService = nivelService;
        }

        // GET: api/listsimple
        [HttpGet("listasimple")]
        public async Task<IEnumerable<NivelSimpleDto>> GetSimple()
        {
            return await _nivelService.SimpleListAsync();
        }
    }
}
