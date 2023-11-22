using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.Permisos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class PermisoController : Controller
    {
        private readonly IPermisoService _permisoService;

        public PermisoController(IPermisoService permisoService)
        {
            _permisoService = permisoService;
        }

        // GET: api/values/2
        [HttpGet("MenusByIdRol/{id}")]
        public async Task<IEnumerable<PermisoDto>> Get(int id)
        {
            return await _permisoService.MenusByIdRolAsync(id);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<PermisoDto>>> Put(int id, [FromBody] PermisoSaveDto saveDto)
        {
            var response = await _permisoService.EditAsync(id, saveDto);

            return TypedResults.Ok(response);
        }
    }
}
