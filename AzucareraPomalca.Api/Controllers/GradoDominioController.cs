using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.GradoDominios;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class GradoDominioController : Controller
    {
        private readonly IGradoDominioService _gradoDominioService;

        public GradoDominioController(IGradoDominioService gradoDominioService)
        {
            _gradoDominioService = gradoDominioService;
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GradoDominioDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<GradoDominioDto>>> Get(int id)
        {
            var response = await _gradoDominioService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GradoDominioDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<GradoDominioDto>>> Post([FromBody] GradoDominioSaveDto saveDto)
        {
            var response = await _gradoDominioService.CreateAsync(saveDto);

            return TypedResults.CreatedAtRoute(response);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<GradoDominioDto> Put(int id, [FromBody] GradoDominioSaveDto saveDto)
        {
            return await _gradoDominioService.EditAsync(id, saveDto);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<GradoDominioDto> Delete(int id)
        {
            return await _gradoDominioService.DisabledAsync(id);
        }

        [HttpGet("FindByNivelAndIdCompetencia")]
        public async Task<GradoDominioDto> PaginatedSearch([FromQuery] GradoDominioFilterDto request)
        {
            return await _gradoDominioService.FindByNivelAndIdCompetenciaAsync(request);
        }
    }
}
