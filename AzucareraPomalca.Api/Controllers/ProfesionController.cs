using AzucareraPomalca.Application.Dtos.Profesiones;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProfesionController : Controller
    {
        private readonly IProfesionService _profesionService;

        public ProfesionController(IProfesionService profesionService)
        {
            _profesionService = profesionService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<ProfesionDto>> Get()
        {
            return await _profesionService.FindAllAsync();
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        public async Task<ProfesionDto> Get(int id)
        {
            return await _profesionService.FindByIdAsync(id);
        }
    }
}
