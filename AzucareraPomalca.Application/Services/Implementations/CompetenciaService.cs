using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Competencias;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class CompetenciaService : ICompetenciaService
    {
        private readonly ICompetenciaRepository _competenciaRepository;
        private readonly IMapper _mapper;

        public CompetenciaService(ICompetenciaRepository competenciaRepository, IMapper mapper)
        {
            _competenciaRepository = competenciaRepository;
            _mapper = mapper;
        }

        public Task<CompetenciaDto> CreateAsync(CompetenciaSaveDto saveDto)
        {
            throw new NotImplementedException();
        }

        public Task<CompetenciaDto> DisabledAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CompetenciaDto> EditAsync(int id, CompetenciaSaveDto saveDto)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<CompetenciaDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PageResponse<CompetenciaDto>> FindAllPaginatedAsync(PageRequest<CompetenciaFilterDto> request)
        {
            var filter = request.Filter ?? new CompetenciaFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Competencia, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Descripcion) || x.Descripcion.ToUpper().Contains(filter.Descripcion.ToUpper()))
                && (string.IsNullOrWhiteSpace(filter.Codigo) || x.Codigo.ToUpper().Contains(filter.Codigo.ToUpper()))
                && (string.IsNullOrWhiteSpace(filter.Nombre) || x.Nombre.ToUpper().Contains(filter.Nombre.ToUpper()))
                && (!filter.IdTipoCompetencia.HasValue || x.IdTipoCompetencia == filter.IdTipoCompetencia);

            List<Expression<Func<Competencia, object>>>? includes = new List<Expression<Func<Competencia, object>>>()
            {
                t => t.TipoCompetencia,
                t => t.GradoDominios
            };

            var response = await _competenciaRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, includes: includes);

            return _mapper.Map<PageResponse<CompetenciaDto>>(response);
        }

        public Task<CompetenciaDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
