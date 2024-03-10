using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Empleados;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<EmpleadoDto>> Get()
        {
            return await _empleadoService.FindAllAsync();
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmpleadoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<EmpleadoDto>>> Post([FromBody] EmpleadoSaveDto saveDto)
        {
            var response = await _empleadoService.CreateAsync(saveDto);

            return TypedResults.CreatedAtRoute(response);
        }

        // POST api/values
        [HttpPost("registromasivo")]
        //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmpleadoDto))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public Task<RespuestaSimpleDto> Masivo([FromBody] List<EmpleadoSaveDto> listSaveDto)
        {
            var response = _empleadoService.CreateMassiveAsync(listSaveDto);

            return response;

            //return TypedResults.CreatedAtRoute(response);

            //foreach (var saveDto in listSaveDto)
            //{
            //    await _empleadoService.CreateAsync(saveDto);
            //}
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<EmpleadoDto> Put(int id, [FromBody] EmpleadoSaveDto saveDto)
        {
            return await _empleadoService.EditAsync(id, saveDto);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<EmpleadoDto> Delete(int id)
        {
            return await _empleadoService.DisabledAsync(id);
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmpleadoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<EmpleadoDto>>> Get(int id)
        {
            var response = await _empleadoService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }

        [HttpGet("PaginatedSearch")]
        public async Task<PageResponse<EmpleadoDto>> PaginatedSearch([FromQuery] PageRequest<EmpleadoFilterDto> request)
        {
            return await _empleadoService.FindAllPaginatedAsync(request);
        }
    }
}
