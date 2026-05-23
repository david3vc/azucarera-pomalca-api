using AzucareraPomalca.Application.Dtos.Equivalencias;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class EquivalenciaService : IEquivalenciaService
    {
        private readonly ICapacitacionRepository _capacitacionRepository;
        private readonly ICursoCompetenciaRepository _cursoCompetenciaRepository;
        private readonly IGradoDominioRepository _gradoDominioRepository;
        private readonly IPerfilCompetenciaEmpleadoRepository _pceRepository;
        private readonly IEmpleadoCursoRepository _empleadoCursoRepository;
        private readonly IPuestoCursoRepository _puestoCursoRepository;
        private readonly IEmpleadoRepository _empleadoRepository;

        public EquivalenciaService(
            ICapacitacionRepository capacitacionRepository,
            ICursoCompetenciaRepository cursoCompetenciaRepository,
            IGradoDominioRepository gradoDominioRepository,
            IPerfilCompetenciaEmpleadoRepository pceRepository,
            IEmpleadoCursoRepository empleadoCursoRepository,
            IPuestoCursoRepository puestoCursoRepository,
            IEmpleadoRepository empleadoRepository)
        {
            _capacitacionRepository = capacitacionRepository;
            _cursoCompetenciaRepository = cursoCompetenciaRepository;
            _gradoDominioRepository = gradoDominioRepository;
            _pceRepository = pceRepository;
            _empleadoCursoRepository = empleadoCursoRepository;
            _puestoCursoRepository = puestoCursoRepository;
            _empleadoRepository = empleadoRepository;
        }

        // SUPUESTO de deduplicación: si un empleado tiene varias Capacitaciones aprobadas
        // del mismo curso, se toma la MÁS RECIENTE por FechaInicio y se cuentan sus NumeroHoras.
        // Para cambiar la política (p.ej. sumar todas las ejecuciones, o limitar por vigencia),
        // modificar SOLO este método.
        private async Task<decimal> HorasAcumuladasPorCursoAsync(int idEmpleado, int idCurso)
        {
            var capacitaciones = await _capacitacionRepository.FindAllAsync(
                predicate: c => c.IdCurso == idCurso
                                && c.CapacitacionEmpleados.Any(ce => ce.IdEmpleado == idEmpleado && ce.Aprobado == true)
            );

            var masReciente = capacitaciones
                .OrderByDescending(c => c.FechaInicio ?? DateTime.MinValue)
                .FirstOrDefault();

            return masReciente?.NumeroHoras ?? 0m;
        }

        public async Task<int?> CalcularNivelAsync(int idEmpleado, int idCompetencia)
        {
            var vinculos = await _cursoCompetenciaRepository.FindByCompetenciaAsync(idCompetencia);

            decimal horasReales = 0m;
            decimal horasEquivalentes = 0m;

            foreach (var v in vinculos)
            {
                var horas = await HorasAcumuladasPorCursoAsync(idEmpleado, v.IdCurso);
                horasReales += horas;
                horasEquivalentes += horas * v.Factor;
            }

            var grados = await _gradoDominioRepository.FindAllAsync(
                predicate: g => g.IdCompetencia == idCompetencia && g.State,
                orderBy: q => q.OrderBy(g => g.Nivel)
            );

            // El nivel alcanzado es el grado MÁS EXIGENTE cuyo umbral ya se cumplió, medido por
            // HorasRequeridas (el umbral real), NO por el número de nivel. Así el cálculo es
            // agnóstico a la convención de numeración: funciona tanto si "más difícil = nivel
            // alto" (ascendente) como si "más difícil = Nivel 1" (descendente, ver R6/D-016).
            GradoDominio? gradoAlcanzado = grados
                .Where(g => g.HorasRequeridas <= horasEquivalentes)
                .OrderByDescending(g => g.HorasRequeridas)
                .FirstOrDefault();

            var pce = await _pceRepository.FindFirstOrDefaultAsync(
                predicate: x => x.IdEmpleado == idEmpleado && x.IdCompetencia == idCompetencia,
                disableTracking: false
            );

            if (pce is null)
            {
                pce = new PerfilCompetenciaEmpleado
                {
                    IdEmpleado = idEmpleado,
                    IdCompetencia = idCompetencia,
                    IdGradoDominio = gradoAlcanzado?.Id,
                    HorasReales = horasReales,
                    HorasEquivalentes = horasEquivalentes,
                    FechaCalculo = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    State = true
                };
            }
            else
            {
                pce.IdGradoDominio = gradoAlcanzado?.Id;
                pce.HorasReales = horasReales;
                pce.HorasEquivalentes = horasEquivalentes;
                pce.FechaCalculo = DateTime.UtcNow;
                pce.UpdatedAt = DateTime.UtcNow;
            }

            await _pceRepository.SaveAsync(pce);

            return gradoAlcanzado?.Nivel;
        }

        public async Task RecalcularHorasCursoAsync(int idEmpleado, int idCurso)
        {
            var horas = await HorasAcumuladasPorCursoAsync(idEmpleado, idCurso);

            var empleadoCurso = await _empleadoCursoRepository.FindFirstOrDefaultAsync(
                predicate: x => x.IdEmpleado == idEmpleado && x.IdCurso == idCurso,
                disableTracking: false
            );

            if (empleadoCurso is null)
            {
                if (horas <= 0m) return; // nada que registrar

                empleadoCurso = new EmpleadoCurso
                {
                    IdEmpleado = idEmpleado,
                    IdCurso = idCurso,
                    HorasAcumuladas = horas,
                    FechaCalculo = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    State = true
                };
            }
            else
            {
                empleadoCurso.HorasAcumuladas = horas;
                empleadoCurso.FechaCalculo = DateTime.UtcNow;
                empleadoCurso.UpdatedAt = DateTime.UtcNow;
            }

            await _empleadoCursoRepository.SaveAsync(empleadoCurso);
        }

        public async Task RecalcularPorCapacitacionAsync(int idCapacitacion)
        {
            var capacitacion = await _capacitacionRepository.FindByIdAsync(idCapacitacion);
            if (capacitacion is null) return;

            var empleadosAprobados = capacitacion.CapacitacionEmpleados
                .Where(ce => ce.Aprobado == true)
                .Select(ce => ce.IdEmpleado)
                .Distinct()
                .ToList();

            if (empleadosAprobados.Count == 0) return;

            // HORAS-DURAS-DESACTIVADO (D-022): los cursos duros (Específicos/SSOMMA/RSE) pasaron a
            // ser binarios (lo lleva o no); el cumplimiento lo decide CapacitacionEmpleado.Aprobado
            // y la fila binaria en empleado_curso. Ya NO se recalculan horas acumuladas duras.
            // Para REVERTIR a horas, descomentar este loop (RecalcularHorasCursoAsync sigue intacto).
            //foreach (var idEmpleado in empleadosAprobados)
            //{
            //    await RecalcularHorasCursoAsync(idEmpleado, capacitacion.IdCurso);
            //}

            // Eje blando: si el curso está vinculado a competencias, recalcular niveles.
            var vinculos = await _cursoCompetenciaRepository.FindByCursoAsync(capacitacion.IdCurso);
            foreach (var v in vinculos)
            {
                foreach (var idEmpleado in empleadosAprobados)
                {
                    await CalcularNivelAsync(idEmpleado, v.IdCompetencia);
                }
            }
        }

        public async Task<RegularizarResultDto> RegularizarAsync()
        {
            var result = new RegularizarResultDto();

            // Eje blando: PCE con IdGradoDominio != null y HorasEquivalentes == 0 → sembrar
            var pceALegacy = await _pceRepository.FindAllAsync(
                predicate: x => x.IdGradoDominio != null && x.HorasEquivalentes == 0m,
                disableTracking: false
            );

            foreach (var pce in pceALegacy)
            {
                var grado = await _gradoDominioRepository.FindByIdAsync(pce.IdGradoDominio!.Value);
                if (grado is null) { result.Omitidos++; continue; }

                pce.HorasEquivalentes = grado.HorasRequeridas;
                pce.HorasReales = grado.HorasRequeridas;
                pce.FechaCalculo = DateTime.UtcNow;
                pce.UpdatedAt = DateTime.UtcNow;
                await _pceRepository.SaveAsync(pce);
                result.ProcesadosBlandos++;
            }

            // HORAS-DURAS-DESACTIVADO (D-022): el eje duro pasó a binario (lo lleva o no). Ya no se
            // siembran horas acumuladas duras desde puesto_curso.horas_requeridas. Se conserva el
            // bloque (comentado) y las columnas en BD para poder REVERTIR a horas en el futuro.
            //var ecLegacy = await _empleadoCursoRepository.FindAllAsync(
            //    predicate: x => x.HorasAcumuladas == 0m,
            //    disableTracking: false
            //);
            //
            //foreach (var ec in ecLegacy)
            //{
            //    var empleado = await _empleadoRepository.FindByIdAsync(ec.IdEmpleado);
            //    if (empleado is null) { result.Omitidos++; continue; }
            //
            //    var pc = await _puestoCursoRepository.FindFirstOrDefaultAsync(
            //        predicate: x => x.IdPuesto == empleado.IdPuesto && x.IdCurso == ec.IdCurso
            //    );
            //
            //    if (pc is null) { result.Omitidos++; continue; }
            //
            //    ec.HorasAcumuladas = pc.HorasRequeridas;
            //    ec.FechaCalculo = DateTime.UtcNow;
            //    ec.UpdatedAt = DateTime.UtcNow;
            //    await _empleadoCursoRepository.SaveAsync(ec);
            //    result.ProcesadosDuros++;
            //}

            return result;
        }
    }
}
