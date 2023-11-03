using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.GrupoOcupacionales;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class GrupoOcupacionalService : IGrupoOcupacionalService
    {
        private readonly IGrupoOcupacionalRepository _grupoOcupacionalRepository;
        private readonly IMapper _mapper;

        public GrupoOcupacionalService(IGrupoOcupacionalRepository grupoOcupacionalRepository, IMapper mapper)
        {
            _grupoOcupacionalRepository = grupoOcupacionalRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<GrupoOcupacionalDto>> FindAllAsync()
        {
            IReadOnlyList<GrupoOcupacional> grupoOcupacionales = await _grupoOcupacionalRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<GrupoOcupacionalDto>>(grupoOcupacionales);
        }

        public async Task<GrupoOcupacionalDto> FindByIdAsync(int id)
        {
            GrupoOcupacional? grupoOcupacional = await _grupoOcupacionalRepository.FindByIdAsync(id);

            if (grupoOcupacional is null) throw TipoProfesionNotFound(id);

            return _mapper.Map<GrupoOcupacionalDto>(grupoOcupacional);
        }

        private NotFoundCoreException TipoProfesionNotFound(int id)
        {
            return new NotFoundCoreException("Grupo ocupacional no encontrado para el id: " + id);
        }
    }
}
