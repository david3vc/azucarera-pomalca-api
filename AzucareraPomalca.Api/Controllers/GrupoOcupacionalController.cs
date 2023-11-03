using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.GrupoOcupacionales;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class GrupoOcupacionalController : Controller
    {
        private readonly IGrupoOcupacionalService _grupoOcupacionalService;

        public GrupoOcupacionalController(IGrupoOcupacionalService grupoOcupacionalService)
        {
            _grupoOcupacionalService = grupoOcupacionalService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<GrupoOcupacionalDto>> Get()
        {
            return await _grupoOcupacionalService.FindAllAsync();
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GrupoOcupacionalDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<GrupoOcupacionalDto>>> Get(int id)
        {
            var response = await _grupoOcupacionalService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }
    }
}
