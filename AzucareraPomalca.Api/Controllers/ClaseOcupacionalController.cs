using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.ClaseOcupacionales;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClaseOcupacionalController : Controller
    {
        private readonly IClaseOcupacionalService _claseOcupacionalService;

        public ClaseOcupacionalController(IClaseOcupacionalService claseOcupacionalService)
        {
            _claseOcupacionalService = claseOcupacionalService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<ClaseOcupacionalDto>> Get()
        {
            return await _claseOcupacionalService.FindAllAsync();
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClaseOcupacionalDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<ClaseOcupacionalDto>>> Get(int id)
        {
            var response = await _claseOcupacionalService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }
    }
}
