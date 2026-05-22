using AzucareraPomalca.Application.Dtos.Brechas;
using AzucareraPomalca.Application.Dtos.Empleados;

namespace AzucareraPomalca.Application.Services
{
    public interface IBrechaService
    {
        /// <summary>
        /// Competencias con brecha blanda en el alcance dado (puesto la exige ∧ el
        /// empleado no alcanzó el nivel requerido), con nº de empleados y cursos
        /// candidatos, ordenado por demanda descendente.
        /// </summary>
        Task<List<BrechaBlandaDto>> BrechasBlandasAsync(BrechaBlandaFilterDto filter);

        /// <summary>
        /// Empleados con brecha en UNA competencia blanda (para auto-precargar los
        /// participantes al planear una capacitación que la cierra).
        /// </summary>
        Task<List<EmpleadoSugeridoDto>> EmpleadosConBrechaBlandaAsync(BrechaBlandaEmpleadosFilterDto filter);
    }
}
