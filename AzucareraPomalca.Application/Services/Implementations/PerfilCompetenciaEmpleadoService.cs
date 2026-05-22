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
        private readonly IGradoDominioRepository _gradoDominioRepository;

        public PerfilCompetenciaEmpleadoService(IMapper mapper, IPerfilCompetenciaEmpleadoRepository perfilCompetenciaEmpleadoRepository, IGradoDominioRepository gradoDominioRepository)
        {
            _mapper = mapper;
            _perfilCompetenciaEmpleadoRepository = perfilCompetenciaEmpleadoRepository;
            _gradoDominioRepository = gradoDominioRepository;
        }

        public async Task<PerfilCompetenciaEmpleadoDto> CreateAsync(PerfilCompetenciaEmpleadoSaveDto saveDto)
        {
            PerfilCompetenciaEmpleado perfilCompetenciaEmpleado = _mapper.Map<PerfilCompetenciaEmpleado>(saveDto);
            await InferirIdCompetenciaAsync(perfilCompetenciaEmpleado);
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

            await InferirIdCompetenciaAsync(perfilCompetenciaEmpleado);

            perfilCompetenciaEmpleado.UpdatedAt = DateTime.UtcNow;

            await _perfilCompetenciaEmpleadoRepository.SaveAsync(perfilCompetenciaEmpleado);

            return _mapper.Map<PerfilCompetenciaEmpleadoDto>(perfilCompetenciaEmpleado);
        }

        // El frontend (PerfilCompetencias.tsx) solo envía IdGradoDominio; la competencia
        // se infiere del grado (su fuente de verdad: GradoDominio.IdCompetencia). Sin esto
        // se persistiría IdCompetencia=0 y reventaría la FK a competencia (500). En edición,
        // AutoMapper sobrescribe el IdCompetencia cargado con 0, por eso se re-infiere aquí.
        // Ver CONTEXTO.md D-017.
        private async Task InferirIdCompetenciaAsync(PerfilCompetenciaEmpleado perfil)
        {
            if (perfil.IdGradoDominio.HasValue && perfil.IdCompetencia == 0)
            {
                GradoDominio? grado = await _gradoDominioRepository.FindByIdAsync(perfil.IdGradoDominio.Value);
                if (grado != null) perfil.IdCompetencia = grado.IdCompetencia;
            }
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
