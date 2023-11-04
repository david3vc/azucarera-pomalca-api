using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.Gerencias;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class GerenciaController : Controller
    {
        private readonly IGerenciaService _gerenciaService;

        public GerenciaController(IGerenciaService gerenciaService)
        {
            _gerenciaService = gerenciaService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<GerenciaDto>> Get()
        {
            return await _gerenciaService.FindAllAsync();
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GerenciaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<GerenciaDto>>> Get(int id)
        {
            var response = await _gerenciaService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }
    }
}
