using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Gerencias;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class GerenciaController : Controller
    {
        private readonly IGerenciaService _gerenciaService;

        public GerenciaController(IGerenciaService gerenciaService)
        {
            _gerenciaService = gerenciaService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<GerenciaDto>> Get()
        {
            return await _gerenciaService.FindAllAsync();
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GerenciaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<GerenciaDto>>> Post([FromBody] GerenciaSaveDto saveDto)
        {
            var response = await _gerenciaService.CreateAsync(saveDto);

            return TypedResults.CreatedAtRoute(response);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<GerenciaDto> Put(int id, [FromBody] GerenciaSaveDto saveDto)
        {
            return await _gerenciaService.EditAsync(id, saveDto);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<GerenciaDto> Delete(int id)
        {
            return await _gerenciaService.DisabledAsync(id);
        }

        // GET: api/listsimple
        [HttpGet("listasimple")]
        public async Task<IEnumerable<GerenciaSimpleDto>> GetSimple()
        {
            return await _gerenciaService.SimpleListAsync();
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GerenciaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<GerenciaDto>>> Get(int id)
        {
            var response = await _gerenciaService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }

        [HttpGet("PaginatedSearch")]
        public async Task<PageResponse<GerenciaDto>> PaginatedSearch([FromQuery] PageRequest<GerenciaFilterDto> request)
        {
            return await _gerenciaService.FindAllPaginatedAsync(request);
        }
    }
}
