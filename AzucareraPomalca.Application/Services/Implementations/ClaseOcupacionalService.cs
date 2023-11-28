using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.ClaseOcupacionales;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class ClaseOcupacionalService : IClaseOcupacionalService
    {
        private readonly IClaseOcupacionalRepository _claseOcupacionalRepository;
        private readonly IMapper _mapper;

        public ClaseOcupacionalService(IClaseOcupacionalRepository claseOcupacionalRepository, IMapper mapper)
        {
            _claseOcupacionalRepository = claseOcupacionalRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ClaseOcupacionalDto>> FindAllAsync()
        {
            IReadOnlyList<ClaseOcupacional> claseOcupacionales = await _claseOcupacionalRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ClaseOcupacionalDto>>(claseOcupacionales);
        }

        public async Task<ClaseOcupacionalDto> FindByIdAsync(int id)
        {
            ClaseOcupacional? claseOcupacional = await _claseOcupacionalRepository.FindByIdAsync(id);

            if (claseOcupacional is null) throw ClaseOcupacionalNotFound(id);

            return _mapper.Map<ClaseOcupacionalDto>(claseOcupacional);
        }

        public async Task<IReadOnlyList<ClaseOcupacionalSimpleDto>> SimpleListAsync()
        {
            IReadOnlyList<ClaseOcupacional> claseOcupacionales = await _claseOcupacionalRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ClaseOcupacionalSimpleDto>>(claseOcupacionales);
        }

        public async Task<IReadOnlyList<ClaseOcupacionalSimpleDto>> SimpleListByIdGrupoOcupacionalAsync(int id)
        {
            Expression<Func<ClaseOcupacional, bool>>? predicate = x => x.IdGrupoOcupacional == id;

            IReadOnlyList<ClaseOcupacional> claseOcupacionales = await _claseOcupacionalRepository.FindAllAsync(predicate: predicate);

            return _mapper.Map<IReadOnlyList<ClaseOcupacionalSimpleDto>>(claseOcupacionales);
        }

        private NotFoundCoreException ClaseOcupacionalNotFound(int id)
        {
            return new NotFoundCoreException("Clase ocupacional no encontrada para el id: " + id);
        }
    }
}
