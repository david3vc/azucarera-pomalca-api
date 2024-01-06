using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.PuestosCursos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class PuestoCursoController : Controller
    {
        private readonly IPuestoCursoService _puestoCursoService;

        public PuestoCursoController(IPuestoCursoService puestoCursoService)
        {
            _puestoCursoService = puestoCursoService;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<PuestoCursoDto> Delete(int id)
        {
            return await _puestoCursoService.DisabledAsync(id);
        }

        // GET: api/values/2
        [HttpGet("{idPuesto}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PuestoCursoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<List<PuestoCursoDto>>>> Get(int idPuesto)
        {
            var response = await _puestoCursoService.GetPuestoCursosByIdPuesto(idPuesto);

            return TypedResults.Ok(response);
        }
    }
}
