using AzucareraPomalca.Application.Dtos.Gerencias;
using AzucareraPomalca.Application.Dtos.GradosAcademicos;
using AzucareraPomalca.Application.Services;
using AzucareraPomalca.Application.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class GradoAcademicoController : Controller
    {
        private readonly IGradoAcademicoService _gradoAcademicoService;

        public GradoAcademicoController(IGradoAcademicoService gradoAcademicoService)
        {
            _gradoAcademicoService = gradoAcademicoService;
        }

        // GET: api/listsimple
        [HttpGet("listasimple")]
        public async Task<IEnumerable<GradoAcademicoSimpleDto>> GetSimple()
        {
            return await _gradoAcademicoService.SimpleListAsync();
        }
    }
}
