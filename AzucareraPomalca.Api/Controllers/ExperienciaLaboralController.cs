using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.ExperienciaLaborales;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class ExperienciaLaboralController : Controller
    {
        private readonly IExperienciaLaboralService _experienciaLaboralService;

        public ExperienciaLaboralController(IExperienciaLaboralService experienciaLaboralService)
        {
            _experienciaLaboralService = experienciaLaboralService;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ExperienciaLaboralDto> Delete(int id)
        {
            return await _experienciaLaboralService.DisabledAsync(id);
        }

        // GET: api/values/2
        [HttpGet("{idEmpleado}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExperienciaLaboralDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<List<ExperienciaLaboralDto>>>> Get(int idEmpleado)
        {
            var response = await _experienciaLaboralService.ExperienciaLaboralByIdEmpleado(idEmpleado);

            return TypedResults.Ok(response);
        }
    }
}
