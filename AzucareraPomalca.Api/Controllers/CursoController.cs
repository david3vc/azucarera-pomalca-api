using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Cursos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Contcursolers
{
    [Route("api/[controller]")]
    public class CursoController : Controller
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<CursoDto>> Get()
        {
            return await _cursoService.FindAllAsync();
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CursoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<CursoDto>>> Get(int id)
        {
            var response = await _cursoService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CursoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<CursoDto>>> Post([FromBody] CursoSaveDto saveDto)
        {
            var response = await _cursoService.CreateAsync(saveDto);

            return TypedResults.CreatedAtRoute(response);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<CursoDto> Put(int id, [FromBody] CursoSaveDto saveDto)
        {
            return await _cursoService.EditAsync(id, saveDto);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<CursoDto> Delete(int id)
        {
            return await _cursoService.DisabledAsync(id);
        }

        [HttpGet("PaginatedSearch")]
        public async Task<PageResponse<CursoDto>> PaginatedSearch([FromQuery] PageRequest<CursoFilterDto> request)
        {
            return await _cursoService.FindAllPaginatedAsync(request);
        }
    }
}
