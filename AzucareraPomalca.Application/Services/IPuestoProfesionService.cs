using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.PuestosProfesiones;

namespace AzucareraPomalca.Application.Services
{
    public interface IPuestoProfesionService : ICrudService<PuestoProfesionDto, PuestoProfesionSaveDto, int>
    {
        Task<List<PuestoProfesionDto>> ProfesionesPuestoByIdPuesto(int idPuesto);
    }
}
