using AzucareraPomalca.Application.Dtos.Brechas;
using AzucareraPomalca.Application.Dtos.Empleados;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class BrechaService : IBrechaService
    {
        private readonly IPuestoRepository _puestoRepository;
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IPerfilCompetenciaRepository _perfilCompetenciaRepository;
        private readonly IPerfilCompetenciaEmpleadoRepository _pceRepository;
        private readonly ICompetenciaRepository _competenciaRepository;
        private readonly ICursoCompetenciaRepository _cursoCompetenciaRepository;

        public BrechaService(
            IPuestoRepository puestoRepository,
            IEmpleadoRepository empleadoRepository,
            IPerfilCompetenciaRepository perfilCompetenciaRepository,
            IPerfilCompetenciaEmpleadoRepository pceRepository,
            ICompetenciaRepository competenciaRepository,
            ICursoCompetenciaRepository cursoCompetenciaRepository)
        {
            _puestoRepository = puestoRepository;
            _empleadoRepository = empleadoRepository;
            _perfilCompetenciaRepository = perfilCompetenciaRepository;
            _pceRepository = pceRepository;
            _competenciaRepository = competenciaRepository;
            _cursoCompetenciaRepository = cursoCompetenciaRepository;
        }

        public async Task<List<BrechaBlandaDto>> BrechasBlandasAsync(BrechaBlandaFilterDto filter)
        {
            // 1. Puestos en el alcance.
            var puestos = await _puestoRepository.FindAllAsync(p =>
                (!filter.IdGerencia.HasValue || p.IdGerencia == filter.IdGerencia)
                && (!filter.IdDivision.HasValue || p.IdDivision == filter.IdDivision)
                && (!filter.IdDepartamento.HasValue || p.IdDepartamento == filter.IdDepartamento)
                && (!filter.IdSeccion.HasValue || p.IdSeccion == filter.IdSeccion)
                && (!filter.IdPuesto.HasValue || p.Id == filter.IdPuesto));

            var puestoIds = puestos.Select(p => p.Id).ToList();
            if (puestoIds.Count == 0) return new List<BrechaBlandaDto>();

            // 2. Empleados activos en esos puestos.
            var empleados = await _empleadoRepository.FindAllAsync(e => e.State && puestoIds.Contains(e.IdPuesto));
            var empleadoIds = empleados.Select(e => e.Id).ToList();
            if (empleadoIds.Count == 0) return new List<BrechaBlandaDto>();

            // 3. Competencias que exige cada puesto (perfil con grado → competencia + nivel).
            var perfilIncludes = new List<Expression<Func<PerfilCompetencia, object>>> { p => p.GradoDominio! };
            var perfiles = await _perfilCompetenciaRepository.FindAllAsync(
                predicate: p => puestoIds.Contains(p.IdPuesto) && p.IdGradoDominio != null && p.State,
                includes: perfilIncludes);

            // Brecha medida por HORAS (umbral real), no por número de nivel: agnóstico a la
            // convención de numeración (ver R6/D-016). El requisito de un puesto en una
            // competencia es el umbral MÁS exigente que pide (Max HorasRequeridas).
            var requeridosPorPuesto = perfiles
                .Where(p => p.GradoDominio != null)
                .GroupBy(p => p.IdPuesto)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(p => new RequeridoComp(p.GradoDominio!.IdCompetencia, p.GradoDominio!.HorasRequeridas)).ToList());

            // 4. Horas equivalentes acumuladas por empleado en cada competencia (PCE).
            var pces = await _pceRepository.FindAllAsync(
                predicate: x => empleadoIds.Contains(x.IdEmpleado) && x.State);

            var horasAlcanzadas = new Dictionary<(int, int), decimal>();
            foreach (var pce in pces)
            {
                horasAlcanzadas[(pce.IdEmpleado, pce.IdCompetencia)] = pce.HorasEquivalentes;
            }

            // 5. Por empleado, competencias donde las horas acumuladas < umbral requerido.
            var demanda = new Dictionary<int, HashSet<int>>(); // idCompetencia → empleados
            foreach (var emp in empleados)
            {
                if (!requeridosPorPuesto.TryGetValue(emp.IdPuesto, out var reqs)) continue;

                foreach (var r in reqs)
                {
                    decimal alcanzado = horasAlcanzadas.TryGetValue((emp.Id, r.IdCompetencia), out var h) ? h : 0m;
                    if (alcanzado < r.HorasRequeridas)
                    {
                        if (!demanda.TryGetValue(r.IdCompetencia, out var set))
                        {
                            set = new HashSet<int>();
                            demanda[r.IdCompetencia] = set;
                        }
                        set.Add(emp.Id);
                    }
                }
            }

            if (demanda.Count == 0) return new List<BrechaBlandaDto>();

            // 6. Nombres de competencias.
            var competencias = await _competenciaRepository.FindAllAsync();
            var compById = competencias.ToDictionary(c => c.Id, c => c);

            // 7. DTOs con cursos candidatos (vínculo CursoCompetencia).
            var result = new List<BrechaBlandaDto>();
            foreach (var kv in demanda)
            {
                compById.TryGetValue(kv.Key, out var comp);
                var vinculos = await _cursoCompetenciaRepository.FindByCompetenciaAsync(kv.Key);

                result.Add(new BrechaBlandaDto
                {
                    IdCompetencia = kv.Key,
                    Codigo = comp?.Codigo,
                    Competencia = comp?.Nombre,
                    NroEmpleados = kv.Value.Count,
                    CursosCandidatos = vinculos.Select(v => new CursoCandidatoDto
                    {
                        IdCurso = v.IdCurso,
                        Codigo = v.Curso?.Codigo,
                        Curso = v.Curso?.Descripcion,
                        TipoCurso = v.Curso?.TipoCurso?.Descripcion,
                        Factor = v.Factor
                    }).ToList()
                });
            }

            return result.OrderByDescending(x => x.NroEmpleados).ToList();
        }

        public async Task<List<EmpleadoSugeridoDto>> EmpleadosConBrechaBlandaAsync(BrechaBlandaEmpleadosFilterDto filter)
        {
            // 1. Puestos en el alcance, con sus navs organizacionales (single-level).
            var puestoIncludes = new List<Expression<Func<Puesto, object>>>
            {
                p => p.Gerencia!, p => p.Division!, p => p.Departamento!, p => p.Seccion!
            };
            var puestos = await _puestoRepository.FindAllAsync(
                predicate: p =>
                    (!filter.IdGerencia.HasValue || p.IdGerencia == filter.IdGerencia)
                    && (!filter.IdDivision.HasValue || p.IdDivision == filter.IdDivision)
                    && (!filter.IdDepartamento.HasValue || p.IdDepartamento == filter.IdDepartamento)
                    && (!filter.IdSeccion.HasValue || p.IdSeccion == filter.IdSeccion)
                    && (!filter.IdPuesto.HasValue || p.Id == filter.IdPuesto),
                includes: puestoIncludes);

            var puestoIds = puestos.Select(p => p.Id).ToList();
            if (puestoIds.Count == 0) return new List<EmpleadoSugeridoDto>();
            var puestoById = puestos.ToDictionary(p => p.Id, p => p);

            // 2. Umbral de horas requerido de la competencia objetivo por puesto (el mayor exigido).
            //    Brecha medida por horas (no por número de nivel) → agnóstico a la convención (R6/D-016).
            var perfilIncludes = new List<Expression<Func<PerfilCompetencia, object>>> { p => p.GradoDominio! };
            var perfiles = await _perfilCompetenciaRepository.FindAllAsync(
                predicate: p => puestoIds.Contains(p.IdPuesto) && p.IdGradoDominio != null && p.State,
                includes: perfilIncludes);

            var horasRequeridasPorPuesto = perfiles
                .Where(p => p.GradoDominio != null && p.GradoDominio.IdCompetencia == filter.IdCompetencia)
                .GroupBy(p => p.IdPuesto)
                .ToDictionary(g => g.Key, g => g.Max(p => p.GradoDominio!.HorasRequeridas));

            if (horasRequeridasPorPuesto.Count == 0) return new List<EmpleadoSugeridoDto>();

            // 3. Empleados activos en esos puestos.
            var empleados = await _empleadoRepository.FindAllAsync(e => e.State && puestoIds.Contains(e.IdPuesto));
            var empleadoIds = empleados.Select(e => e.Id).ToList();
            if (empleadoIds.Count == 0) return new List<EmpleadoSugeridoDto>();

            // 4. Horas equivalentes acumuladas por empleado en esa competencia (PCE).
            var pces = await _pceRepository.FindAllAsync(
                predicate: x => empleadoIds.Contains(x.IdEmpleado) && x.IdCompetencia == filter.IdCompetencia && x.State);
            var horasAlcanzadas = new Dictionary<int, decimal>();
            foreach (var pce in pces) horasAlcanzadas[pce.IdEmpleado] = pce.HorasEquivalentes;

            // 5. Empleados cuyas horas acumuladas < umbral requerido por su puesto.
            var result = new List<EmpleadoSugeridoDto>();
            foreach (var emp in empleados)
            {
                if (!horasRequeridasPorPuesto.TryGetValue(emp.IdPuesto, out var requerido)) continue;
                decimal alcanzado = horasAlcanzadas.TryGetValue(emp.Id, out var h) ? h : 0m;
                if (alcanzado >= requerido) continue;

                puestoById.TryGetValue(emp.IdPuesto, out var pst);
                result.Add(new EmpleadoSugeridoDto
                {
                    IdEmpleado = emp.Id,
                    NombreCompleto = $"{emp.Nombres} {emp.AppellidoPaterno} {emp.AppellidoMaterno}".Trim(),
                    Puesto = pst?.Nombre,
                    Gerencia = pst?.Gerencia?.Nombre,
                    Division = pst?.Division?.Nombre,
                    Departamento = pst?.Departamento?.Nombre,
                    Seccion = pst?.Seccion?.Nombre,
                    CursosFaltantes = null
                });
            }

            return result.OrderBy(x => x.NombreCompleto).ToList();
        }

        private readonly record struct RequeridoComp(int IdCompetencia, int HorasRequeridas);
    }
}
