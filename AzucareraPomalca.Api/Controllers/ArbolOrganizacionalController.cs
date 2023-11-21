using AzucareraPomalca.Application.Dtos.ArbolOrganizacional;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class ArbolOrganizacionalController : Controller
    {
        private readonly IArbolOrganizacionalService _arbolOrganizacionalService;

        public ArbolOrganizacionalController(IArbolOrganizacionalService arbolOrganizacionalService)
        {
            _arbolOrganizacionalService = arbolOrganizacionalService;
        }

        [HttpGet]
        public async Task<NodoDto> Get()
        {
            return await _arbolOrganizacionalService.FindNodoPrincipalPadre();
        }

        [HttpGet("FindNodoUnidadOrganizacional")]
        public async Task<NodoDto> Get([FromQuery] NodoFilterDto request)
        {
            return await _arbolOrganizacionalService.FindNodoUnidadOrganizacionByIdUnidadOrganizacionalAsync(request);
        }

        [HttpGet("FindNodosEmpleadosByIdPuesto/{id}")]
        public async Task<List<NodoDto>> Get(int id)
        {
            return await _arbolOrganizacionalService.FindNodoPuestoById(id);
        }
    }
}
