using AzucareraPomalca.Application.Dtos.Responsabilidades;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class ResponsabilidadController : Controller
    {
        private readonly IResponsabilidadService _responsabilidadService;

        public ResponsabilidadController(IResponsabilidadService responsabilidadService)
        {
            _responsabilidadService = responsabilidadService;
        }

        // GET: api/listsimple
        [HttpGet("listasimple")]
        public async Task<IEnumerable<ResponsabilidadSimpleDto>> GetSimple()
        {
            return await _responsabilidadService.SimpleListAsync();
        }
    }
}
