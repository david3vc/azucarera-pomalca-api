using AzucareraPomalca.Application.Dtos.Equivalencias;

namespace AzucareraPomalca.Application.Services
{
    public interface IEquivalenciaService
    {
        Task<int?> CalcularNivelAsync(int idEmpleado, int idCompetencia);
        Task RecalcularHorasCursoAsync(int idEmpleado, int idCurso);
        Task RecalcularPorCapacitacionAsync(int idCapacitacion);
        Task<RegularizarResultDto> RegularizarAsync();
    }
}
