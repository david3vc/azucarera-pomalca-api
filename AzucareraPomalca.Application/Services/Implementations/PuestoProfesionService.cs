using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.PuestosProfesiones;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class PuestoProfesionService : IPuestoProfesionService
    {
        private readonly IPuestoProfesionRepository _puestoProfesionRepository;
        private readonly IMapper _mapper;

        public PuestoProfesionService(IMapper mapper, IPuestoProfesionRepository puestoProfesionRepository)
        {
            _mapper = mapper;
            _puestoProfesionRepository = puestoProfesionRepository;
        }

        public async Task<PuestoProfesionDto> CreateAsync(PuestoProfesionSaveDto saveDto)
        {
            PuestoProfesion puestoProfesion = _mapper.Map<PuestoProfesion>(saveDto);
            puestoProfesion.CreatedAt = DateTime.UtcNow;
            puestoProfesion.State = true;

            await _puestoProfesionRepository.SaveAsync(puestoProfesion);

            return _mapper.Map<PuestoProfesionDto>(puestoProfesion);
        }

        public async Task<PuestoProfesionDto> DisabledAsync(int id)
        {
            PuestoProfesion? puestoProfesion = await _puestoProfesionRepository.FindByIdAsync(id);

            if (puestoProfesion is null) throw PuestoProfesionNotFound(id);

            puestoProfesion.State = !puestoProfesion.State;

            await _puestoProfesionRepository.SaveAsync(puestoProfesion);

            return _mapper.Map<PuestoProfesionDto>(puestoProfesion);
        }

        public async Task<PuestoProfesionDto> EditAsync(int id, PuestoProfesionSaveDto saveDto)
        {
            PuestoProfesion? puestoProfesion = await _puestoProfesionRepository.FindByIdAsync(id);

            if (puestoProfesion is null) throw PuestoProfesionNotFound(id);

            _mapper.Map<PuestoProfesionSaveDto, PuestoProfesion>(saveDto, puestoProfesion);

            puestoProfesion.UpdatedAt = DateTime.UtcNow;

            await _puestoProfesionRepository.SaveAsync(puestoProfesion);

            return _mapper.Map<PuestoProfesionDto>(puestoProfesion);
        }

        public Task<IReadOnlyList<PuestoProfesionDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PuestoProfesionDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private NotFoundCoreException PuestoProfesionNotFound(int id)
        {
            return new NotFoundCoreException("PuestoProfesion no encontrado para el id: " + id);
        }
    }
}
