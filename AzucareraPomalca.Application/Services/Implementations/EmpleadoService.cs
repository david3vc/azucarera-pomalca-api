using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Empleados;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IMapper _mapper;
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IEmpleadoProfesionService _empleadoProfesionService;
        private readonly IExperienciaLaboralService _experienciaLaboralService;

        public EmpleadoService(IMapper mapper, 
                               IEmpleadoRepository empleadoRepository,
                               IEmpleadoProfesionService empleadoProfesionService,
                               IExperienciaLaboralService experienciaLaboralService)
        {
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
            _empleadoProfesionService = empleadoProfesionService;
            _experienciaLaboralService = experienciaLaboralService;
        }

        public async Task<EmpleadoDto> CreateAsync(EmpleadoSaveDto saveDto)
        {
            Empleado empleado = _mapper.Map<Empleado>(saveDto);
            empleado.CreatedAt = DateTime.UtcNow;
            empleado.State = true;

            var response = await _empleadoRepository.SaveAsync(empleado);

            var result = await FindByIdAsync(response.Id);

            return _mapper.Map<EmpleadoDto>(result);
        }

        public async Task<EmpleadoDto> DisabledAsync(int id)
        {
            Empleado? empleado = await _empleadoRepository.FindByIdAsync(id);

            if (empleado is null) throw EmpleadoNotFound(id);

            empleado.State = !empleado.State;

            await _empleadoRepository.SaveAsync(empleado);

            return _mapper.Map<EmpleadoDto>(empleado);
        }

        public async Task<EmpleadoDto> EditAsync(int id, EmpleadoSaveDto saveDto)
        {
            Empleado? empleado = await _empleadoRepository.FindByIdAsync(id);

            if (empleado is null) throw EmpleadoNotFound(id);

            _mapper.Map<EmpleadoSaveDto, Empleado>(saveDto, empleado);

            empleado.UpdatedAt = DateTime.UtcNow;

            await _empleadoRepository.SaveAsync(empleado);

            #region PROFESIONES
            if (saveDto.EmpleadoProfesionesSave != null && saveDto.EmpleadoProfesionesSave.Count > 0)
            {
                foreach (var empleadoProfesion in saveDto.EmpleadoProfesionesSave)
                {
                    if (empleadoProfesion.Id != null && empleadoProfesion.Id != 0)
                    {
                        empleadoProfesion.IdEmpleado = empleado.Id;
                        await _empleadoProfesionService.EditAsync((int)empleadoProfesion.Id, empleadoProfesion);
                    }
                    else
                    {
                        empleadoProfesion.IdEmpleado = empleado.Id;
                        await _empleadoProfesionService.CreateAsync(empleadoProfesion);
                    }
                }
            }
            #endregion

            #region EXPERIENCIA LABORAL
            if (saveDto.ExperienciaLaboralesSave != null && saveDto.ExperienciaLaboralesSave.Count > 0)
            {
                foreach (var experienciaLaboral in saveDto.ExperienciaLaboralesSave)
                {
                    if (experienciaLaboral.Id != null && experienciaLaboral.Id != 0)
                    {
                        experienciaLaboral.IdEmpleado = empleado.Id;
                        await _experienciaLaboralService.EditAsync((int)experienciaLaboral.Id, experienciaLaboral);
                    }
                    else
                    {
                        experienciaLaboral.IdEmpleado = empleado.Id;
                        await _experienciaLaboralService.CreateAsync(experienciaLaboral);
                    }
                }
            }
            #endregion

            return _mapper.Map<EmpleadoDto>(empleado);
        }

        public async Task<IReadOnlyList<EmpleadoDto>> FindAllAsync()
        {
            IReadOnlyList<Empleado> empleados = await _empleadoRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<EmpleadoDto>>(empleados);
        }

        public async Task<PageResponse<EmpleadoDto>> FindAllPaginatedAsync(PageRequest<EmpleadoFilterDto> request)
        {
            var filter = request.Filter ?? new EmpleadoFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Empleado, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Nombres) || x.Nombres.ToUpper().Contains(filter.Nombres.ToUpper()))
                && (string.IsNullOrWhiteSpace(filter.AppellidoPaterno) || x.AppellidoPaterno.ToUpper().Contains(filter.AppellidoPaterno.ToUpper()))
                && (string.IsNullOrWhiteSpace(filter.AppellidoMaterno) || x.AppellidoMaterno.ToUpper().Contains(filter.AppellidoMaterno.ToUpper()))
                && (!filter.IdCondicionEmpleado.HasValue || x.IdCondicionEmpleado == filter.IdCondicionEmpleado)
                && (!filter.State.HasValue || x.State == filter.State);

            List<Expression<Func<Empleado, object>>>? includes = new List<Expression<Func<Empleado, object>>>()
            {
                t => t.Puesto,
                t => t.Puesto.Gerencia,
                t => t.Puesto.Division,
                t => t.Puesto.Departamento,
                t => t.Puesto.Seccion
            };

            var response = await _empleadoRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, includes: includes);

            return _mapper.Map<PageResponse<EmpleadoDto>>(response);
        }

        public async Task<EmpleadoDto> FindByIdAsync(int id)
        {
            Empleado? empleado = await _empleadoRepository.FindByIdAsync(id);

            if (empleado is null) throw EmpleadoNotFound(id);

            return _mapper.Map<EmpleadoDto>(empleado);
        }

        private NotFoundCoreException EmpleadoNotFound(int id)
        {
            return new NotFoundCoreException("Empleado no encontrada para el id: " + id);
        }
    }
}
