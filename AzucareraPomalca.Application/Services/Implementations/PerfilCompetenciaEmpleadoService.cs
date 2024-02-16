using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.PerfilCompetenciaEmpleados;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class PerfilCompetenciaEmpleadoService : IPerfilCompetenciaEmpleadoService
    {
        private readonly IMapper _mapper;
        private readonly IPerfilCompetenciaEmpleadoRepository _perfilCompetenciaEmpleadoRepository;

        public PerfilCompetenciaEmpleadoService(IMapper mapper, IPerfilCompetenciaEmpleadoRepository perfilCompetenciaEmpleadoRepository)
        {
            _mapper = mapper;
            _perfilCompetenciaEmpleadoRepository = perfilCompetenciaEmpleadoRepository;
        }

        public async Task<PerfilCompetenciaEmpleadoDto> CreateAsync(PerfilCompetenciaEmpleadoSaveDto saveDto)
        {
            PerfilCompetenciaEmpleado perfilCompetenciaEmpleado = _mapper.Map<PerfilCompetenciaEmpleado>(saveDto);
            perfilCompetenciaEmpleado.CreatedAt = DateTime.UtcNow;
            perfilCompetenciaEmpleado.State = true;

            await _perfilCompetenciaEmpleadoRepository.SaveAsync(perfilCompetenciaEmpleado);

            return _mapper.Map<PerfilCompetenciaEmpleadoDto>(perfilCompetenciaEmpleado);
        }

        public Task<PerfilCompetenciaEmpleadoDto> DisabledAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PerfilCompetenciaEmpleadoDto> EditAsync(int id, PerfilCompetenciaEmpleadoSaveDto saveDto)
        {
            PerfilCompetenciaEmpleado? perfilCompetenciaEmpleado = await _perfilCompetenciaEmpleadoRepository.FindByIdAsync(id);

            if (perfilCompetenciaEmpleado is null) throw PerfilCompetenciaEmpleadoNotFound(id);

            _mapper.Map<PerfilCompetenciaEmpleadoSaveDto, PerfilCompetenciaEmpleado>(saveDto, perfilCompetenciaEmpleado);

            perfilCompetenciaEmpleado.UpdatedAt = DateTime.UtcNow;

            await _perfilCompetenciaEmpleadoRepository.SaveAsync(perfilCompetenciaEmpleado);

            return _mapper.Map<PerfilCompetenciaEmpleadoDto>(perfilCompetenciaEmpleado);
        }

        public Task<IReadOnlyList<PerfilCompetenciaEmpleadoDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PerfilCompetenciaEmpleadoDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private NotFoundCoreException PerfilCompetenciaEmpleadoNotFound(int id)
        {
            return new NotFoundCoreException("Perfil Competencia Empleado no encontrado para el id: " + id);
        }
    }
}
