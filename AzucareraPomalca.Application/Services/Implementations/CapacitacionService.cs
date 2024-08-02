using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Capacitaciones;
using AzucareraPomalca.Application.Dtos.EmpleadoCursos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class CapacitacionService : ICapacitacionService
    {
        private readonly ICapacitacionRepository _capacitacionRepository;
        private readonly ICapacitacionEmpleadoService _capacitacionEmpleadoService;
        private readonly IEmpleadoCursoService _empleadoCursoService;
        private readonly IMapper _mapper;

        public CapacitacionService(ICapacitacionRepository capacitacionRepository, IMapper mapper, ICapacitacionEmpleadoService capacitacionEmpleadoService, IEmpleadoCursoService empleadoCursoService)
        {
            _capacitacionRepository = capacitacionRepository;
            _mapper = mapper;
            _capacitacionEmpleadoService = capacitacionEmpleadoService;
            _empleadoCursoService = empleadoCursoService;
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

            _mapper.Map<CapacitacionSaveDto, Capacitacion>(saveDto, capacitacion);

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

            _mapper.Map<CapacitacionSaveDto, Capacitacion>(saveDto, capacitacion);

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
                && (!filter.Evaluado.HasValue || x.Evaluado == filter.Evaluado);

            List<Expression<Func<Capacitacion, object>>>? includes = new List<Expression<Func<Capacitacion, object>>>()
            {
                t => t.Modalidad,
                t => t.TipoFacilitador,
                t => t.Curso.TipoCurso
            };

            var response = await _capacitacionRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, includes: includes);

            return _mapper.Map<PageResponse<CapacitacionDto>>(response);
        }

        private NotFoundCoreException CapacitacionNotFound(int id)
        {
            return new NotFoundCoreException("Capacitacion no encontrado para el id: " + id);
        }
    }
}
