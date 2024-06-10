using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.PerfilCompetencias;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class PerfilCompetenciaController : Controller
    {
        private readonly IPerfilCompetenciaService _perfilCompetenciaService;

        public PerfilCompetenciaController(IPerfilCompetenciaService perfilCompetenciaService)
        {
            _perfilCompetenciaService = perfilCompetenciaService;
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PerfilCompetenciaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<PerfilCompetenciaDto>>> Get(int id)
        {
            var response = await _perfilCompetenciaService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PerfilCompetenciaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<PerfilCompetenciaDto>>> Post([FromBody] PerfilCompetenciaSaveDto saveDto)
        {
            var response = await _perfilCompetenciaService.CreateAsync(saveDto);

            return TypedResults.CreatedAtRoute(response);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<PerfilCompetenciaDto> Put(int id, [FromBody] PerfilCompetenciaSaveDto saveDto)
        {
            return await _perfilCompetenciaService.EditAsync(id, saveDto);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<PerfilCompetenciaDto> Delete(int id)
        {
            return await _perfilCompetenciaService.DisabledAsync(id);
        }

        [HttpGet("PaginatedSearch")]
        public async Task<PageResponse<PerfilCompetenciaDto>> PaginatedSearch([FromQuery] PageRequest<PerfilCompetenciaFilterDto> request)
        {
            return await _perfilCompetenciaService.FindAllPaginatedAsync(request);
        }
    }
}
