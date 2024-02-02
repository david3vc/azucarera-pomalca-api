using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Divisiones;
using AzucareraPomalca.Domain.Cores.Models;
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

        public async Task<DivisionDto> CreateAsync(DivisionSaveDto saveDto)
        {
            Division division = _mapper.Map<Division>(saveDto);
            division.CreatedAt = DateTime.UtcNow;
            division.State = true;

            await _divisionRepository.SaveAsync(division);

            return _mapper.Map<DivisionDto>(division);
        }

        public async Task<DivisionDto> DisabledAsync(int id)
        {
            Division? division = await _divisionRepository.FindByIdAsync(id);

            if (division is null) throw DivisionNotFound(id);

            division.State = !division.State;

            await _divisionRepository.SaveAsync(division);

            return _mapper.Map<DivisionDto>(division);
        }

        public async Task<DivisionDto> EditAsync(int id, DivisionSaveDto saveDto)
        {
            Division? division = await _divisionRepository.FindByIdAsync(id);

            if (division is null) throw DivisionNotFound(id);

            _mapper.Map<DivisionSaveDto, Division>(saveDto, division);

            division.UpdatedAt = DateTime.UtcNow;

            await _divisionRepository.SaveAsync(division);

            return _mapper.Map<DivisionDto>(division);
        }

        public async Task<IReadOnlyList<DivisionDto>> FindAllAsync()
        {
            IReadOnlyList<Division> divisiones = await _divisionRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<DivisionDto>>(divisiones);
        }

        public async Task<PageResponse<DivisionDto>> FindAllPaginatedAsync(PageRequest<DivisionFilterDto> request)
        {
            var filter = request.Filter ?? new DivisionFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Division, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Nombre) || x.Nombre.ToUpper().Contains(filter.Nombre.ToUpper()))
                && (!filter.IdGerencia.HasValue || x.IdGerencia == filter.IdGerencia)
                && (!filter.State.HasValue || x.State == filter.State);

            List<Expression<Func<Division, object>>> includes = new List<Expression<Func<Division, object>>>()
            {
                t => t.Gerencia
            };

            var response = await _divisionRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, includes: includes);

            return _mapper.Map<PageResponse<DivisionDto>>(response);
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
