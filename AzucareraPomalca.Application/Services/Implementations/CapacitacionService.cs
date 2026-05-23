using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Capacitaciones;
using AzucareraPomalca.Application.Dtos.EmpleadoCursos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Utils.Constants;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class CapacitacionService : ICapacitacionService
    {
        private readonly ICapacitacionRepository _capacitacionRepository;
        private readonly ICapacitacionEmpleadoService _capacitacionEmpleadoService;
        private readonly IEmpleadoCursoService _empleadoCursoService;
        private readonly IEquivalenciaService _equivalenciaService;
        private readonly ICursoCompetenciaRepository _cursoCompetenciaRepository;
        private readonly IPlanCapacitacionRepository _planRepository;
        private readonly ITablaComunRepository _tablaComunRepository;
        private readonly IMapper _mapper;

        public CapacitacionService(ICapacitacionRepository capacitacionRepository, IMapper mapper, ICapacitacionEmpleadoService capacitacionEmpleadoService, IEmpleadoCursoService empleadoCursoService, IEquivalenciaService equivalenciaService, ICursoCompetenciaRepository cursoCompetenciaRepository, IPlanCapacitacionRepository planRepository, ITablaComunRepository tablaComunRepository)
        {
            _capacitacionRepository = capacitacionRepository;
            _mapper = mapper;
            _capacitacionEmpleadoService = capacitacionEmpleadoService;
            _empleadoCursoService = empleadoCursoService;
            _equivalenciaService = equivalenciaService;
            _cursoCompetenciaRepository = cursoCompetenciaRepository;
            _planRepository = planRepository;
            _tablaComunRepository = tablaComunRepository;
        }

        public Task<IReadOnlyList<CapacitacionDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CapacitacionDto> FindByIdAsync(int id)
        {
            //List<Expression<Func<Capacitacion, object>>>? includes = new List<Expression<Func<Capacitacion, object>>>()
            //{
            //    t => t.Modalidad,
            //    t => t.TipoFacilitador,
            //    t => t.Curso.TipoCurso,
            //    t => t.CapacitacionEmpleados
            //};
            //Expression<Func<Capacitacion, bool>> predicate = x => x.Id == id;

            //Capacitacion? capacitacion = await _capacitacionRepository.FindByIdAsync(predicate: predicate, includes: includes);
            Capacitacion? capacitacion = await _capacitacionRepository.FindByIdAsync(id);

            if (capacitacion is null) throw CapacitacionNotFound(id);

            return _mapper.Map<CapacitacionDto>(capacitacion);
        }

        public async Task<CapacitacionDto> CreateAsync(CapacitacionSaveDto saveDto)
        {
            await ValidarVinculoCompetenciaAsync(saveDto);

            Capacitacion capacitacion = _mapper.Map<Capacitacion>(saveDto);
            capacitacion.CreatedAt = DateTime.UtcNow;
            capacitacion.State = true;
            capacitacion.Evaluado = false;

            await _capacitacionRepository.SaveAsync(capacitacion);

            #region EMPLEADOS
            if (saveDto.CapacitacionEmpleadosSave != null && saveDto.CapacitacionEmpleadosSave.Count > 0)
            {
                foreach (var capacitacionEmpleado in saveDto.CapacitacionEmpleadosSave)
                {
                    if (capacitacionEmpleado.Id != null && capacitacionEmpleado.Id != 0)
                    {
                        capacitacionEmpleado.IdCapacitacion = capacitacion.Id;
                        await _capacitacionEmpleadoService.EditAsync((int)capacitacionEmpleado.Id, capacitacionEmpleado);
                    }
                    else
                    {
                        capacitacionEmpleado.IdCapacitacion = capacitacion.Id;
                        await _capacitacionEmpleadoService.CreateAsync(capacitacionEmpleado);
                    }
                }
            }
            #endregion

            return _mapper.Map<CapacitacionDto>(capacitacion);
        }

        public async Task<CapacitacionDto> EditAsync(int id, CapacitacionSaveDto saveDto)
        {
            Capacitacion? capacitacion = await _capacitacionRepository.FindByIdAsync(id);

            if (capacitacion is null) throw CapacitacionNotFound(id);

            await ValidarVinculoCompetenciaAsync(saveDto);

            // Editar (completar datos diferidos) no reasigna la línea de plan; preservarlo (ver CONTEXTO §3 D-019).
            int? idPlanCapacitacion = capacitacion.IdPlanCapacitacion;
            int? idCompetencia = capacitacion.IdCompetencia;

            _mapper.Map<CapacitacionSaveDto, Capacitacion>(saveDto, capacitacion);

            capacitacion.IdPlanCapacitacion = idPlanCapacitacion;
            capacitacion.IdCompetencia = idCompetencia;

            capacitacion.UpdatedAt = DateTime.UtcNow;
            capacitacion.Evaluado = false;

            await _capacitacionRepository.SaveAsync(capacitacion);

            #region EMPLEADOS
            if (saveDto.CapacitacionEmpleadosSave != null && saveDto.CapacitacionEmpleadosSave.Count > 0)
            {
                foreach (var capacitacionEmpleado in saveDto.CapacitacionEmpleadosSave)
                {
                    if (capacitacionEmpleado.Id != null && capacitacionEmpleado.Id != 0)
                    {
                        capacitacionEmpleado.IdCapacitacion = capacitacion.Id;
                        await _capacitacionEmpleadoService.EditAsync((int)capacitacionEmpleado.Id, capacitacionEmpleado);
                    }
                    else
                    {
                        capacitacionEmpleado.IdCapacitacion = capacitacion.Id;
                        await _capacitacionEmpleadoService.CreateAsync(capacitacionEmpleado);
                    }
                }
            }
            #endregion

            return _mapper.Map<CapacitacionDto>(capacitacion);
        }

        public async Task<CapacitacionDto> EvaluarAsync(int id, CapacitacionSaveDto saveDto)
        {
            Capacitacion? capacitacion = await _capacitacionRepository.FindByIdAsync(id);

            if (capacitacion is null) throw CapacitacionNotFound(id);

            // REGLA R11: una línea de plan en Borrador no se evalúa, solo se edita
            // (de Aprobado en adelante sí). Ver CONTEXTO §3 D-021.
            if (capacitacion.IdPlanCapacitacion.HasValue)
            {
                PlanCapacitacion? plan = await _planRepository.FindByIdAsync(capacitacion.IdPlanCapacitacion.Value);
                int borradorId = await ResolverEstadoBorradorIdAsync();
                if (plan is not null && plan.IdEstadoPlan == borradorId)
                    throw new BadRequestCoreException(
                        "No se puede evaluar una capacitación de un plan en Borrador. " +
                        "Apruebe el plan antes de evaluar; mientras tanto solo puede editarla.");
            }

            // El vínculo al plan no se gestiona al evaluar; preservarlo (ver CONTEXTO §3 D-019).
            int? idPlanCapacitacion = capacitacion.IdPlanCapacitacion;
            int? idCompetencia = capacitacion.IdCompetencia;

            _mapper.Map<CapacitacionSaveDto, Capacitacion>(saveDto, capacitacion);

            capacitacion.IdPlanCapacitacion = idPlanCapacitacion;
            capacitacion.IdCompetencia = idCompetencia;

            capacitacion.UpdatedAt = DateTime.UtcNow;
            capacitacion.Evaluado = true;

            await _capacitacionRepository.SaveAsync(capacitacion);

            #region EMPLEADOS
            if (saveDto.CapacitacionEmpleadosSave != null && saveDto.CapacitacionEmpleadosSave.Count > 0)
            {
                foreach (var capacitacionEmpleado in saveDto.CapacitacionEmpleadosSave)
                {
                    if (capacitacionEmpleado.Aprobado == true)
                    {
                        EmpleadoCursoSaveDto empleadoCursoSave = new EmpleadoCursoSaveDto()
                        {
                            IdCurso = capacitacion.IdCurso,
                            IdEmpleado = capacitacionEmpleado.IdEmpleado,
                        };

                        await _empleadoCursoService.CreateAsync(empleadoCursoSave);
                    }
                    capacitacionEmpleado.IdCapacitacion = capacitacion.Id;
                    await _capacitacionEmpleadoService.EditAsync(capacitacionEmpleado.Id ?? 0, capacitacionEmpleado);
                }
            }
            #endregion

            await _equivalenciaService.RecalcularPorCapacitacionAsync(capacitacion.Id);

            return _mapper.Map<CapacitacionDto>(capacitacion);
        }

        public async Task<CapacitacionDto> DisabledAsync(int id)
        {
            Capacitacion? capacitacion = await _capacitacionRepository.FindByIdAsync(id);

            if (capacitacion is null) throw CapacitacionNotFound(id);

            capacitacion.State = !capacitacion.State;

            await _capacitacionRepository.SaveAsync(capacitacion);

            return _mapper.Map<CapacitacionDto>(capacitacion);
        }

        public async Task<PageResponse<CapacitacionDto>> FindAllPaginatedAsync(PageRequest<CapacitacionFilterDto> request)
        {
            var filter = request.Filter ?? new CapacitacionFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Capacitacion, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Descripcion) || x.Descripcion.ToUpper().Contains(filter.Descripcion.ToUpper()))
                && (!filter.NumeroHoras.HasValue || x.NumeroHoras == filter.NumeroHoras)
                && (!filter.NumeroParticipantes.HasValue || x.NumeroParticipantes == filter.NumeroParticipantes)
                && (!filter.HorasHombre.HasValue || x.HorasHombre == filter.HorasHombre)
                && (!filter.Costo.HasValue || x.Costo == filter.Costo)
                && (!filter.CostoXTrabjador.HasValue || x.CostoXTrabjador == filter.CostoXTrabjador)
                && (!filter.CostoXHorasHombre.HasValue || x.CostoXHorasHombre == filter.CostoXHorasHombre)
                && (!filter.IdCurso.HasValue || x.IdCurso == filter.IdCurso)
                && (!filter.IdTipoFacilitador.HasValue || x.IdTipoFacilitador == filter.IdTipoFacilitador)
                && (!filter.IdModalidad.HasValue || x.IdModalidad == filter.IdModalidad)
                && (!filter.Evaluado.HasValue || x.Evaluado == filter.Evaluado)
                && (string.IsNullOrWhiteSpace(filter.Profesor) || x.Profesor.ToUpper().Contains(filter.Profesor.ToUpper()))
                && (!filter.FechaInicio.HasValue || x.FechaInicio == filter.FechaInicio)
                && (!filter.IdPlanCapacitacion.HasValue || x.IdPlanCapacitacion == filter.IdPlanCapacitacion)
                && (!filter.State.HasValue || x.State == filter.State);

            List<Expression<Func<Capacitacion, object>>>? includes = new List<Expression<Func<Capacitacion, object>>>()
            {
                t => t.Modalidad,
                t => t.TipoFacilitador,
                t => t.Curso.TipoCurso,
                t => t.PlanCapacitacion.EstadoPlan
            };

            Func<IQueryable<Capacitacion>, IOrderedQueryable<Capacitacion>> orderBy =
                q => q.OrderByDescending(c => c.CreatedAt).ThenByDescending(c => c.Id);

            var response = await _capacitacionRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, orderBy: orderBy, includes: includes);

            return _mapper.Map<PageResponse<CapacitacionDto>>(response);
        }

        // R-E: si la línea apunta a una competencia (eje blando), el curso vehículo
        // debe estar vinculado a esa competencia en CursoCompetencia; si no, el cierre
        // de brecha blanda (CalcularNivelAsync) nunca tocaría la competencia objetivo.
        private async Task ValidarVinculoCompetenciaAsync(CapacitacionSaveDto saveDto)
        {
            if (!saveDto.IdCompetencia.HasValue) return;

            CursoCompetencia? vinculo = await _cursoCompetenciaRepository.FindFirstOrDefaultAsync(
                predicate: x => x.IdCurso == saveDto.IdCurso
                                && x.IdCompetencia == saveDto.IdCompetencia.Value
                                && x.State);

            if (vinculo is null)
                throw new BadRequestCoreException(
                    "El curso seleccionado no está vinculado a la competencia objetivo. " +
                    "Vincúlelos en la pantalla de competencias (Cursos que aportan) para que la " +
                    "brecha blanda cierre al evaluar.");
        }

        // Resuelve el id surrogate del estado Borrador por (id_tabla, id_fila) sin
        // hardcodearlo (espeja PlanCapacitacionService.ResolverEstadoIdAsync).
        private async Task<int> ResolverEstadoBorradorIdAsync()
        {
            TablaComun? estado = await _tablaComunRepository.FindFirstOrDefaultAsync(
                predicate: x => x.IdTabla == EstadosPlan.ID_TABLA && x.IdFila == EstadosPlan.FILA_BORRADOR && x.State);

            if (estado is null)
                throw new BadRequestCoreException(
                    "Catálogo Estado del Plan no sembrado. Ejecute 20260522_plan_capacitacion.sql.");

            return estado.Id;
        }

        private NotFoundCoreException CapacitacionNotFound(int id)
        {
            return new NotFoundCoreException("Capacitacion no encontrado para el id: " + id);
        }
    }
}
