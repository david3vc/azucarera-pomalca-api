using AzucareraPomalca.Application.Dtos.TipoProfesiones;
using AzucareraPomalca.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class TipoProfesionController : Controller
    {
        private readonly ITipoProfesionService _tipoProfesionService;

        public TipoProfesionController(ITipoProfesionService tipoProfesionService)
        {
            _tipoProfesionService = tipoProfesionService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<TipoProfesionDto>> Get()
        {
            return await _tipoProfesionService.FindAllAsync();
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        public async Task<TipoProfesionDto> Get(int id)
        {
            return await _tipoProfesionService.FindByIdAsync(id);
        }
    }
}
