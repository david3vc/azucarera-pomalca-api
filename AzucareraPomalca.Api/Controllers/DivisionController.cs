using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.Divisiones;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class DivisionController : Controller
    {
        private readonly IDivisionService _divisionService;

        public DivisionController(IDivisionService divisionService)
        {
            _divisionService = divisionService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<DivisionDto>> Get()
        {
            return await _divisionService.FindAllAsync();
        }

        // GET: api/listsimple
        [HttpGet("listasimple")]
        public async Task<IEnumerable<DivisionSimpleDto>> GetSimple()
        {
            return await _divisionService.SimpleListAsync();
        }

        // GET: api/listasimplebyidgerencia
        [HttpGet("listasimplebyidgerencia/{id}")]
        public async Task<IEnumerable<DivisionSimpleDto>> GetSimpleByIdGerencia(int id)
        {
            return await _divisionService.SimpleListByIdGerenciaAsync(id);
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DivisionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<DivisionDto>>> Get(int id)
        {
            var response = await _divisionService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }
    }
}
