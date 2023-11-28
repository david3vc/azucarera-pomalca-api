using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.Secciones;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class SeccionController : Controller
    {
        private readonly ISeccionService _seccionService;

        public SeccionController(ISeccionService seccionService)
        {
            _seccionService = seccionService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<SeccionDto>> Get()
        {
            return await _seccionService.FindAllAsync();
        }

        // GET: api/listsimple
        [HttpGet("listasimple")]
        public async Task<IEnumerable<SeccionSimpleDto>> GetSimple()
        {
            return await _seccionService.SimpleListAsync();
        }

        // GET: api/listasimplebyids
        [HttpGet("listasimplebyids")]
        public async Task<IEnumerable<SeccionSimpleDto>> GetSimpleByIds([FromQuery] SeccionSimpleFilterDto request)
        {
            return await _seccionService.SimpleListByIdsAsync(request);
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SeccionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<SeccionDto>>> Get(int id)
        {
            var response = await _seccionService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }
    }
}
