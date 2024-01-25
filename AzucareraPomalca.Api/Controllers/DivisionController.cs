using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Divisiones;
using AzucareraPomalca.Application.Dtos.Gerencias;
using AzucareraPomalca.Application.Services;
using AzucareraPomalca.Application.Services.Implementations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class DivisionController : Controller
    {
        private readonly IDivisionService _divisionService;

        public DivisionController(IDivisionService divisionService)
        {
            _divisionService = divisionService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<DivisionDto>> Get()
        {
            return await _divisionService.FindAllAsync();
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DivisionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<DivisionDto>>> Post([FromBody] DivisionSaveDto saveDto)
        {
            var response = await _divisionService.CreateAsync(saveDto);

            return TypedResults.CreatedAtRoute(response);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<DivisionDto> Put(int id, [FromBody] DivisionSaveDto saveDto)
        {
            return await _divisionService.EditAsync(id, saveDto);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<DivisionDto> Delete(int id)
        {
            return await _divisionService.DisabledAsync(id);
        }

        // GET: api/listsimple
        [HttpGet("listasimple")]
        public async Task<IEnumerable<DivisionSimpleDto>> GetSimple()
        {
            return await _divisionService.SimpleListAsync();
        }

        // GET: api/listasimplebyidgerencia
        [HttpGet("listasimplebyidgerencia/{id}")]
        public async Task<IEnumerable<DivisionSimpleDto>> GetSimpleByIdGerencia(int id)
        {
            return await _divisionService.SimpleListByIdGerenciaAsync(id);
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DivisionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<DivisionDto>>> Get(int id)
        {
            var response = await _divisionService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }

        [HttpGet("PaginatedSearch")]
        public async Task<PageResponse<DivisionDto>> PaginatedSearch([FromQuery] PageRequest<DivisionFilterDto> request)
        {
            return await _divisionService.FindAllPaginatedAsync(request);
        }
    }
}
