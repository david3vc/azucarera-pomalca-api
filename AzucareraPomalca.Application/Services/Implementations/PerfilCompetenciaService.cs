using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.PerfilCompetencias;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class PerfilCompetenciaService : IPerfilCompetenciaService
    {
        private readonly IPerfilCompetenciaRepository _perfilCompetenciaRepository;
        private readonly IMapper _mapper;

        public PerfilCompetenciaService(IPerfilCompetenciaRepository perfilCompetenciaRepository, IMapper mapper)
        {
            _perfilCompetenciaRepository = perfilCompetenciaRepository;
            _mapper = mapper;
        }

        public async Task<PerfilCompetenciaDto> CreateAsync(PerfilCompetenciaSaveDto saveDto)
        {
            PerfilCompetencia perfilCompetencia = _mapper.Map<PerfilCompetencia>(saveDto);
            perfilCompetencia.CreatedAt = DateTime.UtcNow;
            perfilCompetencia.State = true;

            await _perfilCompetenciaRepository.SaveAsync(perfilCompetencia);

            return _mapper.Map<PerfilCompetenciaDto>(perfilCompetencia);
        }

        public async Task<PerfilCompetenciaDto> DisabledAsync(int id)
        {
            PerfilCompetencia? perfilCompetencia = await _perfilCompetenciaRepository.FindByIdAsync(id);

            if (perfilCompetencia is null) throw PerfilCompetenciaNotFound(id);

            perfilCompetencia.State = !perfilCompetencia.State;

            await _perfilCompetenciaRepository.SaveAsync(perfilCompetencia);

            return _mapper.Map<PerfilCompetenciaDto>(perfilCompetencia);
        }

        public async Task<PerfilCompetenciaDto> EditAsync(int id, PerfilCompetenciaSaveDto saveDto)
        {
            PerfilCompetencia? perfilCompetencia = await _perfilCompetenciaRepository.FindByIdAsync(id);

            if (perfilCompetencia is null) throw PerfilCompetenciaNotFound(id);

            _mapper.Map<PerfilCompetenciaSaveDto, PerfilCompetencia>(saveDto, perfilCompetencia);

            perfilCompetencia.UpdatedAt = DateTime.UtcNow;

            await _perfilCompetenciaRepository.SaveAsync(perfilCompetencia);

            return _mapper.Map<PerfilCompetenciaDto>(perfilCompetencia);
        }

        public Task<IReadOnlyList<PerfilCompetenciaDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PageResponse<PerfilCompetenciaDto>> FindAllPaginatedAsync(PageRequest<PerfilCompetenciaFilterDto> request)
        {
            var filter = request.Filter ?? new PerfilCompetenciaFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<PerfilCompetencia, bool>> predicate = x =>
                (!filter.IdPuesto.HasValue || x.IdPuesto == filter.IdPuesto)
                && (x.State == true);

            List<Expression<Func<PerfilCompetencia, object>>> includes = new List<Expression<Func<PerfilCompetencia, object>>>()
            {
                t => t.GradoDominio.CompetenciaSimple.TipoCompetencia
            };

            var response = await _perfilCompetenciaRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, includes: includes);

            return _mapper.Map<PageResponse<PerfilCompetenciaDto>>(response);
        }

        public async Task<PerfilCompetenciaDto> FindByIdAsync(int id)
        {
            PerfilCompetencia? PerfilCompetencia = await _perfilCompetenciaRepository.FindByIdAsync(id);

            if (PerfilCompetencia is null) throw PerfilCompetenciaNotFound(id);

            return _mapper.Map<PerfilCompetenciaDto>(PerfilCompetencia);
        }

        private NotFoundCoreException PerfilCompetenciaNotFound(int id)
        {
            return new NotFoundCoreException("PerfilCompetencia no encontrado para el id: " + id);
        }
    }
}
