using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.TipoProfesiones;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class TipoProfesionService : ITipoProfesionService
    {
        private readonly ITipoProfesionRepository _tipoProfesionRepository;
        private readonly IMapper _mapper;

        public TipoProfesionService(ITipoProfesionRepository tipoProfesionRepository, IMapper mapper)
        {
            _tipoProfesionRepository = tipoProfesionRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TipoProfesionDto>> FindAllAsync()
        {
            IReadOnlyList<TipoProfesion> tipoProfesiones = await _tipoProfesionRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<TipoProfesionDto>>(tipoProfesiones);
        }

        public async Task<TipoProfesionDto> FindByIdAsync(int id)
        {
            TipoProfesion? tipoProfesion = await _tipoProfesionRepository.FindByIdAsync(id);

            return _mapper.Map<TipoProfesionDto>(tipoProfesion);
        }

        private NotFoundCoreException TipoProfesionNotFound(int id)
        {
            return new NotFoundCoreException("Tipo profesión no encontrado para el id: " + id);
        }
    }
}
