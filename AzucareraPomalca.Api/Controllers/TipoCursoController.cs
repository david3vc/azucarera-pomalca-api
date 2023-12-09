using AzucareraPomalca.Application.Dtos.TipoCursos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class TipoCursoController : Controller
    {
        private readonly ITipoCursoService _tipoCursoService;

        public TipoCursoController(ITipoCursoService tipoCursoService)
        {
            _tipoCursoService = tipoCursoService;
        }

        // GET: api/listsimple
        [HttpGet("listasimple")]
        public async Task<IEnumerable<TipoCursoSimpleDto>> GetSimple()
        {
            return await _tipoCursoService.SimpleListAsync();
        }
    }
}
