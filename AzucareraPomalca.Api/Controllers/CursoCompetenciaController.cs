using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.CursoCompetencias;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class CursoCompetenciaController : Controller
    {
        private readonly ICursoCompetenciaService _service;

        public CursoCompetenciaController(ICursoCompetenciaService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CursoCompetenciaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<CursoCompetenciaDto>>> Get(int id)
        {
            var response = await _service.FindByIdAsync(id);
            return TypedResults.Ok(response);
        }

        [HttpGet("Competencia/{idCompetencia}")]
        public async Task<List<CursoCompetenciaDto>> GetByCompetencia(int idCompetencia)
        {
            return await _service.FindByCompetenciaAsync(idCompetencia);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CursoCompetenciaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<CursoCompetenciaDto>>> Post([FromBody] CursoCompetenciaSaveDto saveDto)
        {
            var response = await _service.CreateAsync(saveDto);
            return TypedResults.CreatedAtRoute(response);
        }

        [HttpPut("{id}")]
        public async Task<CursoCompetenciaDto> Put(int id, [FromBody] CursoCompetenciaSaveDto saveDto)
        {
            return await _service.EditAsync(id, saveDto);
        }

        [HttpDelete("{id}")]
        public async Task<CursoCompetenciaDto> Delete(int id)
        {
            return await _service.DisabledAsync(id);
        }

        [HttpGet("PaginatedSearch")]
        public async Task<PageResponse<CursoCompetenciaDto>> PaginatedSearch([FromQuery] PageRequest<CursoCompetenciaFilterDto> request)
        {
            return await _service.FindAllPaginatedAsync(request);
        }
    }
}
