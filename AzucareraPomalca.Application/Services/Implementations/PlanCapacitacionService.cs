using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.PlanesCapacitacion;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Utils.Constants;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class PlanCapacitacionService : IPlanCapacitacionService
    {
        private readonly IPlanCapacitacionRepository _planRepository;
        private readonly ITablaComunRepository _tablaComunRepository;
        private readonly IMapper _mapper;

        public PlanCapacitacionService(
            IPlanCapacitacionRepository planRepository,
            ITablaComunRepository tablaComunRepository,
            IMapper mapper)
        {
            _planRepository = planRepository;
            _tablaComunRepository = tablaComunRepository;
            _mapper = mapper;
        }

        public async Task<PlanCapacitacionDto> CreateAsync(PlanCapacitacionSaveDto saveDto)
        {
            // Año único entre planes activos: refleja el índice filtrado
            // IX_plan_capacitacion_anio_activo (anio WHERE state=1). Se valida antes de
            // insertar para devolver un 400 con mensaje claro en vez de un 500 por la
            // violación del constraint en SQL Server.
            PlanCapacitacion? existente = await _planRepository.FindFirstOrDefaultAsync(
                predicate: x => x.Anio == saveDto.Anio && x.State);
            if (existente is not null)
                throw new BadRequestCoreException($"Ya existe un plan activo para el año {saveDto.Anio}.");

            PlanCapacitacion plan = _mapper.Map<PlanCapacitacion>(saveDto);
            plan.CreatedAt = DateTime.UtcNow;
            plan.State = true;
            plan.IdEstadoPlan = await ResolverEstadoIdAsync(EstadosPlan.FILA_BORRADOR);

            await _planRepository.SaveAsync(plan);

            return _mapper.Map<PlanCapacitacionDto>(plan);
        }

        public async Task<PlanCapacitacionDto> EditAsync(int id, PlanCapacitacionSaveDto saveDto)
        {
            PlanCapacitacion? plan = await _planRepository.FindByIdAsync(id);

            if (plan is null) throw PlanNotFound(id);

            int borradorId = await ResolverEstadoIdAsync(EstadosPlan.FILA_BORRADOR);
            if (plan.IdEstadoPlan != borradorId)
                throw new BadRequestCoreException("Sólo se puede editar un plan en estado Borrador.");

            _mapper.Map<PlanCapacitacionSaveDto, PlanCapacitacion>(saveDto, plan);

            plan.UpdatedAt = DateTime.UtcNow;

            await _planRepository.SaveAsync(plan);

            return _mapper.Map<PlanCapacitacionDto>(plan);
        }

        public async Task<PlanCapacitacionDto> DisabledAsync(int id)
        {
            PlanCapacitacion? plan = await _planRepository.FindByIdAsync(id);

            if (plan is null) throw PlanNotFound(id);

            plan.State = !plan.State;
            plan.UpdatedAt = DateTime.UtcNow;

            await _planRepository.SaveAsync(plan);

            return _mapper.Map<PlanCapacitacionDto>(plan);
        }

        public async Task<PlanCapacitacionDto> AprobarAsync(int id)
        {
            PlanCapacitacion? plan = await _planRepository.FindByIdAsync(id);

            if (plan is null) throw PlanNotFound(id);

            plan.IdEstadoPlan = await ResolverEstadoIdAsync(EstadosPlan.FILA_APROBADO);
            plan.FechaAprobacion = DateTime.UtcNow;
            plan.UpdatedAt = DateTime.UtcNow;

            await _planRepository.SaveAsync(plan);

            return _mapper.Map<PlanCapacitacionDto>(plan);
        }

        public async Task<PlanCapacitacionDto> CerrarAsync(int id)
        {
            PlanCapacitacion? plan = await _planRepository.FindByIdAsync(id);

            if (plan is null) throw PlanNotFound(id);

            plan.IdEstadoPlan = await ResolverEstadoIdAsync(EstadosPlan.FILA_CERRADO);
            plan.UpdatedAt = DateTime.UtcNow;

            await _planRepository.SaveAsync(plan);

            return _mapper.Map<PlanCapacitacionDto>(plan);
        }

        public async Task<IReadOnlyList<PlanCapacitacionDto>> FindAllAsync()
        {
            IReadOnlyList<PlanCapacitacion> planes = await _planRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<PlanCapacitacionDto>>(planes);
        }

        public async Task<PlanCapacitacionDto> FindByIdAsync(int id)
        {
            PlanCapacitacion? plan = await _planRepository.FindDetalleByIdAsync(id);

            if (plan is null) throw PlanNotFound(id);

            PlanCapacitacionDto dto = _mapper.Map<PlanCapacitacionDto>(plan);
            dto.Resumen = ConstruirResumen(plan);

            return dto;
        }

        public async Task<PageResponse<PlanCapacitacionDto>> FindAllPaginatedAsync(PageRequest<PlanCapacitacionFilterDto> request)
        {
            var filter = request.Filter ?? new PlanCapacitacionFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<PlanCapacitacion, bool>> predicate = x =>
                (!filter.Anio.HasValue || x.Anio == filter.Anio)
                && (!filter.IdEstadoPlan.HasValue || x.IdEstadoPlan == filter.IdEstadoPlan)
                && (string.IsNullOrWhiteSpace(filter.Descripcion) || (x.Descripcion != null && x.Descripcion.ToUpper().Contains(filter.Descripcion.ToUpper())))
                && (!filter.PresupuestoMinimo.HasValue || x.PresupuestoTotal >= filter.PresupuestoMinimo)
                && (!filter.PresupuestoMaximo.HasValue || x.PresupuestoTotal <= filter.PresupuestoMaximo)
                && (!filter.State.HasValue || x.State == filter.State);

            List<Expression<Func<PlanCapacitacion, object>>>? includes = new List<Expression<Func<PlanCapacitacion, object>>>()
            {
                t => t.EstadoPlan
            };

            Func<IQueryable<PlanCapacitacion>, IOrderedQueryable<PlanCapacitacion>> orderBy =
                q => q.OrderByDescending(p => p.Anio).ThenByDescending(p => p.Id);

            var response = await _planRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, orderBy: orderBy, includes: includes);

            return _mapper.Map<PageResponse<PlanCapacitacionDto>>(response);
        }

        public async Task<PlanResumenDto> CalcularResumenAsync(int id)
        {
            PlanCapacitacion? plan = await _planRepository.FindDetalleByIdAsync(id);

            if (plan is null) throw PlanNotFound(id);

            return ConstruirResumen(plan);
        }

        /// <summary>
        /// Cuadros derivados al vuelo de las líneas y sus participantes reales.
        /// Cortes por programa/modalidad/facilitador desde la línea; por gerencia y
        /// nivel ocupacional desde los participantes (Empleado→Puesto→Gerencia /
        /// ClaseOcupacional→GrupoOcupacional), prorrateando costo y HH por nº de
        /// participantes de cada línea.
        /// </summary>
        private PlanResumenDto ConstruirResumen(PlanCapacitacion plan)
        {
            var caps = (plan.Capacitaciones ?? new List<Capacitacion>())
                .Where(c => c.State)
                .ToList();

            decimal ppto = plan.PresupuestoTotal;
            decimal Pct(decimal costo) => ppto == 0m ? 0m : Math.Round(costo / ppto * 100m, 2);

            var resumen = new PlanResumenDto
            {
                PresupuestoTotal = ppto,
                FuerzaLaboral = plan.FuerzaLaboral,
                CostoTotal = caps.Sum(c => c.Costo),
                HorasHombreTotal = caps.Sum(c => c.HorasHombre),
                NroCursos = caps.Count
            };
            resumen.SaldoPresupuesto = ppto - resumen.CostoTotal;
            resumen.HhPorTrabajador = plan.FuerzaLaboral == 0 ? 0m : Math.Round((decimal)resumen.HorasHombreTotal / plan.FuerzaLaboral, 2);
            resumen.CostoPorHh = resumen.HorasHombreTotal == 0 ? 0m : Math.Round(resumen.CostoTotal / resumen.HorasHombreTotal, 2);

            // Cortes por línea (programa = TipoCurso, modalidad, facilitador).
            resumen.PorPrograma = caps
                .GroupBy(c => c.Curso?.TipoCurso?.Descripcion ?? "Sin programa")
                .Select(g => new ResumenItemDto
                {
                    Etiqueta = g.Key,
                    NroCursos = g.Count(),
                    NroParticipantes = g.Sum(c => c.NumeroParticipantes),
                    HorasHombre = g.Sum(c => c.HorasHombre),
                    Costo = g.Sum(c => c.Costo),
                    PorcentajePresupuesto = Pct(g.Sum(c => c.Costo))
                })
                .OrderByDescending(x => x.Costo).ToList();

            resumen.PorModalidad = caps
                .GroupBy(c => c.Modalidad?.Descripcion ?? "Sin modalidad")
                .Select(g => new ResumenItemDto
                {
                    Etiqueta = g.Key,
                    NroCursos = g.Count(),
                    NroParticipantes = g.Sum(c => c.NumeroParticipantes),
                    HorasHombre = g.Sum(c => c.HorasHombre),
                    Costo = g.Sum(c => c.Costo),
                    PorcentajePresupuesto = Pct(g.Sum(c => c.Costo))
                })
                .OrderByDescending(x => x.Costo).ToList();

            resumen.PorFacilitador = caps
                .GroupBy(c => c.TipoFacilitador?.Descripcion ?? "Sin facilitador")
                .Select(g => new ResumenItemDto
                {
                    Etiqueta = g.Key,
                    NroCursos = g.Count(),
                    NroParticipantes = g.Sum(c => c.NumeroParticipantes),
                    HorasHombre = g.Sum(c => c.HorasHombre),
                    Costo = g.Sum(c => c.Costo),
                    PorcentajePresupuesto = Pct(g.Sum(c => c.Costo))
                })
                .OrderByDescending(x => x.Costo).ToList();

            // Cortes por participante (gerencia, nivel ocupacional) con costo/HH prorrateados.
            var pares = caps.SelectMany(c =>
            {
                var emps = (c.CapacitacionEmpleados ?? new List<CapacitacionEmpleado>())
                    .Where(ce => ce.State && ce.Empleado != null)
                    .ToList();
                int n = emps.Count;
                decimal costoUnit = n == 0 ? 0m : c.Costo / n;
                decimal hhUnit = n == 0 ? 0m : (decimal)c.HorasHombre / n;

                return emps.Select(ce => new
                {
                    Gerencia = ce.Empleado!.Puesto?.Gerencia?.Nombre ?? "Sin gerencia",
                    Nivel = ce.Empleado!.Puesto?.ClaseOcupacional?.GrupoOcupacional?.Nombre ?? "Sin nivel",
                    Costo = costoUnit,
                    Hh = hhUnit
                });
            }).ToList();

            resumen.PorGerencia = pares
                .GroupBy(p => p.Gerencia)
                .Select(g => new ResumenItemDto
                {
                    Etiqueta = g.Key,
                    NroParticipantes = g.Count(),
                    HorasHombre = (int)Math.Round(g.Sum(p => p.Hh)),
                    Costo = Math.Round(g.Sum(p => p.Costo), 2),
                    PorcentajePresupuesto = Pct(g.Sum(p => p.Costo))
                })
                .OrderByDescending(x => x.Costo).ToList();

            resumen.PorNivel = pares
                .GroupBy(p => p.Nivel)
                .Select(g => new ResumenItemDto
                {
                    Etiqueta = g.Key,
                    NroParticipantes = g.Count(),
                    HorasHombre = (int)Math.Round(g.Sum(p => p.Hh)),
                    Costo = Math.Round(g.Sum(p => p.Costo), 2),
                    PorcentajePresupuesto = Pct(g.Sum(p => p.Costo))
                })
                .OrderByDescending(x => x.Costo).ToList();

            return resumen;
        }

        /// <summary>
        /// Resuelve el id surrogate (id_tabla_comun) de un estado del plan a partir de
        /// (EstadosPlan.ID_TABLA, idFila), sin hardcodear el id.
        /// </summary>
        private async Task<int> ResolverEstadoIdAsync(int idFila)
        {
            TablaComun? estado = await _tablaComunRepository.FindFirstOrDefaultAsync(
                predicate: x => x.IdTabla == EstadosPlan.ID_TABLA && x.IdFila == idFila && x.State);

            if (estado is null)
                throw new BadRequestCoreException(
                    $"Catálogo Estado del Plan no sembrado (id_tabla={EstadosPlan.ID_TABLA}, id_fila={idFila}). " +
                    "Ejecute la migración 20260522_plan_capacitacion.sql.");

            return estado.Id;
        }

        private NotFoundCoreException PlanNotFound(int id)
        {
            return new NotFoundCoreException("Plan de Capacitación no encontrado para el id: " + id);
        }
    }
}
