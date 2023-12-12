using AutoMapper;
using AzucareraPomalca.Application.Dtos.TipoCompetencias;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class TipoCompetenciaService : ITipoCompetenciaService
    {
        private readonly ITipoCompetenciaRepository _tipoCompetenciaRepository;
        private readonly IMapper _mapper;

        public TipoCompetenciaService(ITipoCompetenciaRepository tipoCompetenciaRepository, IMapper mapper)
        {
            _tipoCompetenciaRepository = tipoCompetenciaRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TipoCompetenciaDto>> SimpleListAsync()
        {
            IReadOnlyList<TipoCompetencia> tipoCompetencias = await _tipoCompetenciaRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<TipoCompetenciaDto>>(tipoCompetencias);
        }
    }
}
