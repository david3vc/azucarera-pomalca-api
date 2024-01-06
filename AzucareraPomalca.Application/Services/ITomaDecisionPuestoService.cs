using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.TomaDecisionPuestos;

namespace AzucareraPomalca.Application.Services
{
    public interface ITomaDecisionPuestoService : ICrudService<TomaDecisionPuestoDto, TomaDecisionPuestoSaveDto, int>
    {
        Task<List<TomaDecisionPuestoDto>> GetTomaDecisionPuestosByIdPuesto(int idPuesto);
    }
}
