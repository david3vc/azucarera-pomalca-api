using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Coordinaciones;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class CoordinacionService : ICoordinacionService
    {
        private readonly ICoordinacionRepository _coordinacionRepository;
        private readonly IMapper _mapper;

        public CoordinacionService(ICoordinacionRepository coordinacionRepository, IMapper mapper)
        {
            _coordinacionRepository = coordinacionRepository;
            _mapper = mapper;
        }

        public async Task<CoordinacionDto> CreateAsync(CoordinacionSaveDto saveDto)
        {
            Coordinacion coordinacion = _mapper.Map<Coordinacion>(saveDto);
            coordinacion.CreatedAt = DateTime.UtcNow;
            coordinacion.State = true;

            await _coordinacionRepository.SaveAsync(coordinacion);

            return _mapper.Map<CoordinacionDto>(coordinacion);
        }

        public async Task<CoordinacionDto> DisabledAsync(int id)
        {
            Coordinacion? coordinacion = await _coordinacionRepository.FindByIdAsync(id);

            if (coordinacion is null) throw CoordinacionNotFound(id);

            coordinacion.State = !coordinacion.State;

            await _coordinacionRepository.SaveAsync(coordinacion);

            return _mapper.Map<CoordinacionDto>(coordinacion);
        }

        public async Task<CoordinacionDto> EditAsync(int id, CoordinacionSaveDto saveDto)
        {
            Coordinacion? coordinacion = await _coordinacionRepository.FindByIdAsync(id);

            if (coordinacion is null) throw CoordinacionNotFound(id);

            _mapper.Map<CoordinacionSaveDto, Coordinacion>(saveDto, coordinacion);

            coordinacion.UpdatedAt = DateTime.UtcNow;

            await _coordinacionRepository.SaveAsync(coordinacion);

            return _mapper.Map<CoordinacionDto>(coordinacion);
        }

        public Task<IReadOnlyList<CoordinacionDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CoordinacionDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private NotFoundCoreException CoordinacionNotFound(int id)
        {
            return new NotFoundCoreException("Coordinacion no encontrado para el id: " + id);
        }
    }
}
