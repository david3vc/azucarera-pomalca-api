using AzucareraPomalca.Application.Dtos.FuncionesEspecificas;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class FuncionEspecificaController : Controller
    {
        private readonly IFuncionEspecificaService _funcionEspecificaService;

        public FuncionEspecificaController(IFuncionEspecificaService funcionEspecificaService)
        {
            _funcionEspecificaService = funcionEspecificaService;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<FuncionEspecificaDto> Delete(int id)
        {
            return await _funcionEspecificaService.DisabledAsync(id);
        }
    }
}
