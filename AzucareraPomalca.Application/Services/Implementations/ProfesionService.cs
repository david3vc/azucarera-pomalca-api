using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Profesiones;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class ProfesionService : IProfesionService
    {
        private readonly IProfesionRepository _profesionRepository;
        private readonly IMapper _mapper;

        public ProfesionService(IProfesionRepository profesionRepository, IMapper mapper)
        {
            _profesionRepository = profesionRepository;
            _mapper = mapper;
        }

        public async Task<ProfesionDto> CreateAsync(ProfesionSaveDto saveDto)
        {
            Profesion profesion = _mapper.Map<Profesion>(saveDto);
            profesion.CreatedAt = DateTime.UtcNow;
            profesion.State = true;

            await _profesionRepository.SaveAsync(profesion);

            return _mapper.Map<ProfesionDto>(profesion);
        }

        public async Task<ProfesionDto> DisabledAsync(int id)
        {
            Profesion? profesion = await _profesionRepository.FindByIdAsync(id);

            if (profesion is null) throw ProfesionNotFound(id);

            profesion.State = !profesion.State;

            await _profesionRepository.SaveAsync(profesion);

            return _mapper.Map<ProfesionDto>(profesion);
        }

        public async Task<ProfesionDto> EditAsync(int id, ProfesionSaveDto saveDto)
        {
            Profesion? profesion = await _profesionRepository.FindByIdAsync(id);

            if (profesion is null) throw ProfesionNotFound(id);

            _mapper.Map<ProfesionSaveDto, Profesion>(saveDto, profesion);

            profesion.UpdatedAt = DateTime.UtcNow;

            await _profesionRepository.SaveAsync(profesion);

            return _mapper.Map<ProfesionDto>(profesion);
        }

        public async Task<IReadOnlyList<ProfesionDto>> FindAllAsync()
        {
            IReadOnlyList<Profesion> profesiones = await _profesionRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ProfesionDto>>(profesiones);
        }

        public async Task<PageResponse<ProfesionDto>> FindAllPaginatedAsync(PageRequest<ProfesionFilterDto> request)
        {
            var filter = request.Filter ?? new ProfesionFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Profesion, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Codigo) || x.Codigo.ToUpper().Contains(filter.Codigo.ToUpper()))
                && (string.IsNullOrWhiteSpace(filter.Nombre) || x.Nombre.ToUpper().Contains(filter.Nombre.ToUpper()))
                && (!filter.IdTipoProfesion.HasValue || x.IdTipoProfesion == filter.IdTipoProfesion)
                && (!filter.State.HasValue || x.State == filter.State);

            List<Expression<Func<Profesion, object>>> includes = new List<Expression<Func<Profesion, object>>>()
            {
                t => t.TipoProfesion
            };

            var response = await _profesionRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, includes: includes);

            return _mapper.Map<PageResponse<ProfesionDto>>(response);
        }

        public async Task<ProfesionDto> FindByIdAsync(int id)
        {
            Profesion? profesion = await _profesionRepository.FindByIdAsync(id);

            if (profesion is null) throw ProfesionNotFound(id);

            return _mapper.Map<ProfesionDto>(profesion);
        }

        private NotFoundCoreException ProfesionNotFound(int id)
        {
            return new NotFoundCoreException("Profesion no encontrada para el id: " + id);
        }
    }
}
