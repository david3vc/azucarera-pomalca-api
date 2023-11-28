using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.Departamentos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class DepartamentoController : Controller
    {
        private readonly IDepartamentoService _departamentoService;

        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<DepartamentoDto>> Get()
        {
            return await _departamentoService.FindAllAsync();
        }

        // GET: api/listsimple
        [HttpGet("listasimple")]
        public async Task<IEnumerable<DepartamentoSimpleDto>> GetSimple()
        {
            return await _departamentoService.SimpleListAsync();
        }

        // GET: api/listasimplebyids
        [HttpGet("listasimplebyids")]
        public async Task<IEnumerable<DepartamentoSimpleDto>> GetSimpleByIds([FromQuery] DepartamentoSimpleFilterDto request)
        {
            return await _departamentoService.SimpleListByIdsAsync(request);
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartamentoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<DepartamentoDto>>> Get(int id)
        {
            var response = await _departamentoService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }
    }
}
