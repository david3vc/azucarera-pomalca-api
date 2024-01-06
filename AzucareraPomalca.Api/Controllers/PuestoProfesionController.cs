using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.PuestosProfesiones;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class PuestoProfesionController : Controller
    {
        private readonly IPuestoProfesionService _puestoProfesionService;

        public PuestoProfesionController(IPuestoProfesionService puestoProfesionService)
        {
            _puestoProfesionService = puestoProfesionService;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<PuestoProfesionDto> Delete(int id)
        {
            return await _puestoProfesionService.DisabledAsync(id);
        }

        // GET: api/values/2
        [HttpGet("{idPuesto}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PuestoProfesionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<List<PuestoProfesionDto>>>> Get(int idPuesto)
        {
            var response = await _puestoProfesionService.ProfesionesPuestoByIdPuesto(idPuesto);

            return TypedResults.Ok(response);
        }
    }
}
