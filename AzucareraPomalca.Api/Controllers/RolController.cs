using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Roles;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class RolController : Controller
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<RolDto>> Get()
        {
            return await _rolService.FindAllAsync();
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RolDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<RolDto>>> Get(int id)
        {
            var response = await _rolService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RolDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<RolDto>>> Post([FromBody] RolSaveDto saveDto)
        {
            var response = await _rolService.CreateAsync(saveDto);

            return TypedResults.CreatedAtRoute(response);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<RolDto> Put(int id, [FromBody] RolSaveDto saveDto)
        {
            return await _rolService.EditAsync(id, saveDto);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<RolDto> Delete(int id)
        {
            return await _rolService.DisabledAsync(id);
        }

        [HttpGet("PaginatedSearch")]
        public async Task<PageResponse<RolDto>> PaginatedSearch([FromQuery] PageRequest<RolFilterDto> request)
        {
            return await _rolService.FindAllPaginatedAsync(request);
        }
    }
}
