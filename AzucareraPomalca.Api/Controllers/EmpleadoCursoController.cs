using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.EmpleadoCursos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmpleadoCursoController : Controller
    {
        private readonly IEmpleadoCursoService _empleadoCursoService;

        public EmpleadoCursoController(IEmpleadoCursoService empleadoCursoService)
        {
            _empleadoCursoService = empleadoCursoService;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<EmpleadoCursoDto> Delete(int id)
        {
            return await _empleadoCursoService.DisabledAsync(id);
        }

        // GET: api/values/2
        [HttpGet("{idEmpleado}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmpleadoCursoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<List<EmpleadoCursoDto>>>> Get(int idEmpleado)
        {
            var response = await _empleadoCursoService.CursosEmpleadoByIdEmpleado(idEmpleado);

            return TypedResults.Ok(response);
        }
    }
}
