using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Capacitaciones;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class CapacitacionController : Controller
    {
        private readonly ICapacitacionService _capacitacionService;

        public CapacitacionController(ICapacitacionService capacitacionService)
        {
            _capacitacionService = capacitacionService;
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CapacitacionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<CapacitacionDto>>> Get(int id)
        {
            var response = await _capacitacionService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CapacitacionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<CapacitacionDto>>> Post([FromBody] CapacitacionSaveDto saveDto)
        {
            var response = await _capacitacionService.CreateAsync(saveDto);

            return TypedResults.CreatedAtRoute(response);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<CapacitacionDto> Put(int id, [FromBody] CapacitacionSaveDto saveDto)
        {
            return await _capacitacionService.EditAsync(id, saveDto);
        }

        // EVLUAR api/values/5
        [HttpPut("Evaluar/{id}")]
        public async Task<CapacitacionDto> Evaluar(int id, [FromBody] CapacitacionSaveDto saveDto)
        {
            return await _capacitacionService.EvaluarAsync(id, saveDto);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<CapacitacionDto> Delete(int id)
        {
            return await _capacitacionService.DisabledAsync(id);
        }

        [HttpGet("PaginatedSearch")]
        public async Task<PageResponse<CapacitacionDto>> PaginatedSearch([FromQuery] PageRequest<CapacitacionFilterDto> request)
        {
            return await _capacitacionService.FindAllPaginatedAsync(request);
        }
    }
}
