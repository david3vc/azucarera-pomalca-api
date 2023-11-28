using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Divisiones;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class DivisionService : IDivisionService
    {
        private readonly IDivisionRepository _divisionRepository;
        private readonly IMapper _mapper;

        public DivisionService(IDivisionRepository divisionRepository, IMapper mapper)
        {
            _divisionRepository = divisionRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<DivisionDto>> FindAllAsync()
        {
            IReadOnlyList<Division> divisiones = await _divisionRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<DivisionDto>>(divisiones);
        }

        public async Task<DivisionDto> FindByIdAsync(int id)
        {
            Division? division = await _divisionRepository.FindByIdAsync(id);

            if (division is null) throw DivisionNotFound(id);

            return _mapper.Map<DivisionDto>(division);
        }

        public async Task<IReadOnlyList<DivisionSimpleDto>> SimpleListAsync()
        {
            IReadOnlyList<Division> divisiones = await _divisionRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<DivisionSimpleDto>>(divisiones);
        }

        public async Task<IReadOnlyList<DivisionSimpleDto>> SimpleListByIdGerenciaAsync(int id)
        {
            Expression<Func<Division, bool>>? predicate = x => x.IdGerencia == id;

            IReadOnlyList<Division> claseOcupacionales = await _divisionRepository.FindAllAsync(predicate: predicate);

            return _mapper.Map<IReadOnlyList<DivisionSimpleDto>>(claseOcupacionales);
        }

        private NotFoundCoreException DivisionNotFound(int id)
        {
            return new NotFoundCoreException("Division no encontrada para el id: " + id);
        }
    }
}
