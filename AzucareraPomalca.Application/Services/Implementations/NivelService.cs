using AutoMapper;
using AzucareraPomalca.Application.Dtos.Niveles;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class NivelService : INivelService
    {
        private readonly INivelRepository _nivelRepository;
        private readonly IMapper _mapper;

        public NivelService(INivelRepository nivelRepository, IMapper mapper)
        {
            _nivelRepository = nivelRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<NivelSimpleDto>> SimpleListAsync()
        {
            IReadOnlyList<Nivel> niveles = await _nivelRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<NivelSimpleDto>>(niveles);
        }
    }
}
