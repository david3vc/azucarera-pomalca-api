using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Profesiones;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProfesionDto))]
        public async Task<IEnumerable<ProfesionDto>> Get()
        {
            return await _profesionService.FindAllAsync();
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProfesionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<ProfesionDto>>> Get(int id)
        {
            var response = await _profesionService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProfesionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<ProfesionDto>>> Post([FromBody] ProfesionSaveDto saveDto)
        {
            var response = await _profesionService.CreateAsync(saveDto);

            return TypedResults.CreatedAtRoute(response);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ProfesionDto> Put(int id, [FromBody] ProfesionSaveDto saveDto)
        {
            return await _profesionService.EditAsync(id, saveDto);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ProfesionDto> Delete(int id)
        {
            return await _profesionService.DisabledAsync(id);
        }

        [HttpGet("PaginatedSearch")]
        public async Task<PageResponse<ProfesionDto>> PaginatedSearch([FromQuery] PageRequest<ProfesionFilterDto> request)
        {
            return await _profesionService.FindAllPaginatedAsync(request);
        }
    }
}
