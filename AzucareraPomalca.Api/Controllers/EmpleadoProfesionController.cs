using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.EmpleadoProfesiones;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmpleadoProfesionController : Controller
    {
        private readonly IEmpleadoProfesionService _empleadoProfesionService;

        public EmpleadoProfesionController(IEmpleadoProfesionService empleadoProfesionService)
        {
            _empleadoProfesionService = empleadoProfesionService;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<EmpleadoProfesionDto> Delete(int id)
        {
            return await _empleadoProfesionService.DisabledAsync(id);
        }

        // GET: api/values/2
        [HttpGet("{idEmpleado}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EmpleadoProfesionDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<List<EmpleadoProfesionDto>>>> Get(int idEmpleado)
        {
            var response = await _empleadoProfesionService.ProfesionesEmpleadoByIdEmpleado(idEmpleado);

            return TypedResults.Ok(response);
        }
    }
}
