using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.EmpleadoCursos;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class EmpleadoCursoService : IEmpleadoCursoService
    {
        private readonly IEmpleadoCursoRepository _empleadoCursoRepository;
        private readonly IMapper _mapper;

        public EmpleadoCursoService(IEmpleadoCursoRepository empleadoCursoRepository, IMapper mapper)
        {
            _empleadoCursoRepository = empleadoCursoRepository;
            _mapper = mapper;
        }

        public async Task<EmpleadoCursoDto> CreateAsync(EmpleadoCursoSaveDto saveDto)
        {
            EmpleadoCurso empleadoCurso = _mapper.Map<EmpleadoCurso>(saveDto);
            empleadoCurso.CreatedAt = DateTime.UtcNow;
            empleadoCurso.State = true;

            await _empleadoCursoRepository.SaveAsync(empleadoCurso);

            return _mapper.Map<EmpleadoCursoDto>(empleadoCurso);
        }

        public async Task<EmpleadoCursoDto> DisabledAsync(int id)
        {
            EmpleadoCurso? empleadoCurso = await _empleadoCursoRepository.FindByIdAsync(id);

            if (empleadoCurso is null) throw EmpleadoCursoNotFound(id);

            empleadoCurso.State = false;

            await _empleadoCursoRepository.SaveAsync(empleadoCurso);

            return _mapper.Map<EmpleadoCursoDto>(empleadoCurso);
        }

        public async Task<EmpleadoCursoDto> EditAsync(int id, EmpleadoCursoSaveDto saveDto)
        {
            EmpleadoCurso? empleadoCurso = await _empleadoCursoRepository.FindByIdAsync(id);

            if (empleadoCurso is null) throw EmpleadoCursoNotFound(id);

            _mapper.Map<EmpleadoCursoSaveDto, EmpleadoCurso>(saveDto, empleadoCurso);

            empleadoCurso.UpdatedAt = DateTime.UtcNow;

            await _empleadoCursoRepository.SaveAsync(empleadoCurso);

            return _mapper.Map<EmpleadoCursoDto>(empleadoCurso);
        }

        public Task<IReadOnlyList<EmpleadoCursoDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmpleadoCursoDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private NotFoundCoreException EmpleadoCursoNotFound(int id)
        {
            return new NotFoundCoreException("Empleado curso no encontrado para el id: " + id);
        }
    }
}
