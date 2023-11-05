using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.Puestos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class PuestoController : Controller
    {
        private readonly IPuestoService _puestoService;

        public PuestoController(IPuestoService puestoService)
        {
            _puestoService = puestoService;
        }

        // GET: api/values
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PuestoDto))]
        public async Task<IEnumerable<PuestoDto>> Get()
        {
            return await _puestoService.FindAllAsync();
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PuestoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<PuestoDto>>> Get(int id)
        {
            var response = await _puestoService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PuestoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<PuestoDto>>> Post([FromBody] PuestoSaveDto saveDto)
        {
            var response = await _puestoService.CreateAsync(saveDto);

            return TypedResults.CreatedAtRoute(response);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<PuestoDto> Put(int id, [FromBody] PuestoSaveDto saveDto)
        {
            return await _puestoService.EditAsync(id, saveDto);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<PuestoDto> Delete(int id)
        {
            return await _puestoService.DisabledAsync(id);
        }
    }
}
