using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.PlanesCapacitacion;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class PlanCapacitacionController : Controller
    {
        private readonly IPlanCapacitacionService _planService;

        public PlanCapacitacionController(IPlanCapacitacionService planService)
        {
            _planService = planService;
        }

        // GET: api/plancapacitacion/2  → plan + detalle + resumen
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlanCapacitacionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<PlanCapacitacionDto>>> Get(int id)
        {
            var response = await _planService.FindByIdAsync(id);

            return TypedResults.Ok(response);
        }

        // GET: api/plancapacitacion/2/resumen  → sólo cuadros
        [HttpGet("{id}/resumen")]
        public async Task<PlanResumenDto> GetResumen(int id)
        {
            return await _planService.CalcularResumenAsync(id);
        }

        // POST api/plancapacitacion  → crea cabecera (estado Borrador)
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PlanCapacitacionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<PlanCapacitacionDto>>> Post([FromBody] PlanCapacitacionSaveDto saveDto)
        {
            var response = await _planService.CreateAsync(saveDto);

            return TypedResults.CreatedAtRoute(response);
        }

        // PUT api/plancapacitacion/5  → edita (sólo Borrador)
        [HttpPut("{id}")]
        public async Task<PlanCapacitacionDto> Put(int id, [FromBody] PlanCapacitacionSaveDto saveDto)
        {
            return await _planService.EditAsync(id, saveDto);
        }

        // PUT api/plancapacitacion/aprobar/5
        [HttpPut("Aprobar/{id}")]
        public async Task<PlanCapacitacionDto> Aprobar(int id)
        {
            return await _planService.AprobarAsync(id);
        }

        // PUT api/plancapacitacion/cerrar/5
        [HttpPut("Cerrar/{id}")]
        public async Task<PlanCapacitacionDto> Cerrar(int id)
        {
            return await _planService.CerrarAsync(id);
        }

        // DELETE api/plancapacitacion/5  → toggle State
        [HttpDelete("{id}")]
        public async Task<PlanCapacitacionDto> Delete(int id)
        {
            return await _planService.DisabledAsync(id);
        }

        [HttpGet("PaginatedSearch")]
        public async Task<PageResponse<PlanCapacitacionDto>> PaginatedSearch([FromQuery] PageRequest<PlanCapacitacionFilterDto> request)
        {
            return await _planService.FindAllPaginatedAsync(request);
        }
    }
}
