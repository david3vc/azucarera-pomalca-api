using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Competencias;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class CompetenciaController : Controller
    {
        private readonly ICompetenciaService _competenciaService;

        public CompetenciaController(ICompetenciaService competenciaService)
        {
            _competenciaService = competenciaService;
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompetenciaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<CompetenciaDto>>> Get(int id)
        {
            var response = await _competenciaService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CompetenciaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<CompetenciaDto>>> Post([FromBody] CompetenciaSaveDto saveDto)
        {
            var response = await _competenciaService.CreateAsync(saveDto);

            return TypedResults.CreatedAtRoute(response);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<CompetenciaDto> Put(int id, [FromBody] CompetenciaSaveDto saveDto)
        {
            return await _competenciaService.EditAsync(id, saveDto);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<CompetenciaDto> Delete(int id)
        {
            return await _competenciaService.DisabledAsync(id);
        }

        [HttpGet("PaginatedSearch")]
        public async Task<PageResponse<CompetenciaDto>> PaginatedSearch([FromQuery] PageRequest<CompetenciaFilterDto> request)
        {
            return await _competenciaService.FindAllPaginatedAsync(request);
        }
    }
}
