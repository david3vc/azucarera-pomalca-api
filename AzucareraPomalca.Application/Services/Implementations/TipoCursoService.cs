using AutoMapper;
using AzucareraPomalca.Application.Dtos.TipoCursos;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class TipoCursoService : ITipoCursoService
    {
        private readonly ITipoCursoRepository _tipoCursoRepository;
        private readonly IMapper _mapper;

        public TipoCursoService(IMapper mapper, ITipoCursoRepository tipoCursoRepository)
        {
            _mapper = mapper;
            _tipoCursoRepository = tipoCursoRepository;
        }

        public async Task<IReadOnlyList<TipoCursoSimpleDto>> SimpleListAsync()
        {
            IReadOnlyList<TipoCurso> tipoCursos = await _tipoCursoRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<TipoCursoSimpleDto>>(tipoCursos);
        }
    }
}
