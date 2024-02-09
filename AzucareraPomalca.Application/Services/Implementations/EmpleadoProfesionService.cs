using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.EmpleadoProfesiones;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class EmpleadoProfesionService : IEmpleadoProfesionService
    {
        private readonly IEmpleadoProfesionRepository _empleadoProfesionRepository;
        private readonly IMapper _mapper;

        public EmpleadoProfesionService(IEmpleadoProfesionRepository empleadoProfesionRepository, IMapper mapper)
        {
            _empleadoProfesionRepository = empleadoProfesionRepository;
            _mapper = mapper;
        }

        public async Task<EmpleadoProfesionDto> CreateAsync(EmpleadoProfesionSaveDto saveDto)
        {
            EmpleadoProfesion empleadoProfesion = _mapper.Map<EmpleadoProfesion>(saveDto);
            empleadoProfesion.CreatedAt = DateTime.UtcNow;
            empleadoProfesion.State = true;

            Expression<Func<EmpleadoProfesion, bool>> predicate = x => x.IdProfesion == saveDto.IdProfesion && x.IdEmpleado == saveDto.IdEmpleado;

            var validar = await _empleadoProfesionRepository.FindByIdAsync(predicate);

            if (validar != null)
            {
                if (validar.State == false)
                {
                    var save = _mapper.Map<EmpleadoProfesionSaveDto>(validar);
                    return await EditAsync(validar.Id, save);
                }
                else throw new BadRequestCoreException("Ya se registó la misma carrera con el mismo grado académico.");
            }
            else
            {
                await _empleadoProfesionRepository.SaveAsync(empleadoProfesion);
                return _mapper.Map<EmpleadoProfesionDto>(empleadoProfesion);
            }
        }

        public async Task<EmpleadoProfesionDto> DisabledAsync(int id)
        {
            EmpleadoProfesion? empleadoProfesion = await _empleadoProfesionRepository.FindByIdAsync(id);

            if (empleadoProfesion is null) throw EmpleadoProfesionNotFound(id);

            empleadoProfesion.State = false;

            await _empleadoProfesionRepository.SaveAsync(empleadoProfesion);

            return _mapper.Map<EmpleadoProfesionDto>(empleadoProfesion);
        }

        public async Task<EmpleadoProfesionDto> EditAsync(int id, EmpleadoProfesionSaveDto saveDto)
        {
            EmpleadoProfesion? empleadoProfesion = await _empleadoProfesionRepository.FindByIdAsync(id);

            if (empleadoProfesion is null) throw EmpleadoProfesionNotFound(id);

            _mapper.Map<EmpleadoProfesionSaveDto, EmpleadoProfesion>(saveDto, empleadoProfesion);

            empleadoProfesion.UpdatedAt = DateTime.UtcNow;
            empleadoProfesion.State = true;

            Expression<Func<EmpleadoProfesion, bool>> predicate = x => x.IdProfesion == saveDto.IdProfesion && x.IdProfesion == saveDto.IdProfesion;

            var validar = await _empleadoProfesionRepository.FindByIdAsync(predicate);

            if (validar != null)
            {
                if (validar.State == true && validar.Id != empleadoProfesion.Id) throw new BadRequestCoreException("Ya se registó la misma carrera con el mismo grado académico.");
                else if (validar.Id == empleadoProfesion.Id) await _empleadoProfesionRepository.SaveAsync(empleadoProfesion);
            }
            else
            {
                await _empleadoProfesionRepository.SaveAsync(empleadoProfesion);
            }

            return _mapper.Map<EmpleadoProfesionDto>(empleadoProfesion);
        }

        public Task<IReadOnlyList<EmpleadoProfesionDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmpleadoProfesionDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private NotFoundCoreException EmpleadoProfesionNotFound(int id)
        {
            return new NotFoundCoreException("Registro no encontrado para el id: " + id);
        }
    }
}
