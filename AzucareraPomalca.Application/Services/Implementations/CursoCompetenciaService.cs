using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.CursoCompetencias;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class CursoCompetenciaService : ICursoCompetenciaService
    {
        private readonly IMapper _mapper;
        private readonly ICursoCompetenciaRepository _cursoCompetenciaRepository;

        public CursoCompetenciaService(IMapper mapper, ICursoCompetenciaRepository cursoCompetenciaRepository)
        {
            _mapper = mapper;
            _cursoCompetenciaRepository = cursoCompetenciaRepository;
        }

        public async Task<CursoCompetenciaDto> CreateAsync(CursoCompetenciaSaveDto saveDto)
        {
            CursoCompetencia entity = _mapper.Map<CursoCompetencia>(saveDto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.State = true;

            await _cursoCompetenciaRepository.SaveAsync(entity);

            return _mapper.Map<CursoCompetenciaDto>(entity);
        }

        public async Task<CursoCompetenciaDto> DisabledAsync(int id)
        {
            CursoCompetencia? entity = await _cursoCompetenciaRepository.FindByIdAsync(id);
            if (entity is null) throw NotFound(id);

            entity.State = !entity.State;
            await _cursoCompetenciaRepository.SaveAsync(entity);

            return _mapper.Map<CursoCompetenciaDto>(entity);
        }

        public async Task<CursoCompetenciaDto> EditAsync(int id, CursoCompetenciaSaveDto saveDto)
        {
            CursoCompetencia? entity = await _cursoCompetenciaRepository.FindByIdAsync(id);
            if (entity is null) throw NotFound(id);

            _mapper.Map<CursoCompetenciaSaveDto, CursoCompetencia>(saveDto, entity);
            entity.UpdatedAt = DateTime.UtcNow;

            await _cursoCompetenciaRepository.SaveAsync(entity);

            return _mapper.Map<CursoCompetenciaDto>(entity);
        }

        public Task<IReadOnlyList<CursoCompetenciaDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CursoCompetenciaDto> FindByIdAsync(int id)
        {
            CursoCompetencia? entity = await _cursoCompetenciaRepository.FindByIdAsync(id);
            if (entity is null) throw NotFound(id);

            return _mapper.Map<CursoCompetenciaDto>(entity);
        }

        public async Task<List<CursoCompetenciaDto>> FindByCompetenciaAsync(int idCompetencia)
        {
            var data = await _cursoCompetenciaRepository.FindByCompetenciaAsync(idCompetencia);
            return _mapper.Map<List<CursoCompetenciaDto>>(data);
        }

        public async Task<PageResponse<CursoCompetenciaDto>> FindAllPaginatedAsync(PageRequest<CursoCompetenciaFilterDto> request)
        {
            var filter = request.Filter ?? new CursoCompetenciaFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<CursoCompetencia, bool>> predicate = x =>
                (!filter.IdCurso.HasValue || x.IdCurso == filter.IdCurso)
                && (!filter.IdCompetencia.HasValue || x.IdCompetencia == filter.IdCompetencia);

            List<Expression<Func<CursoCompetencia, object>>>? includes = new List<Expression<Func<CursoCompetencia, object>>>()
            {
                t => t.Curso.TipoCurso,
                t => t.Competencia
            };

            var response = await _cursoCompetenciaRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, includes: includes);

            return _mapper.Map<PageResponse<CursoCompetenciaDto>>(response);
        }

        private NotFoundCoreException NotFound(int id)
        {
            return new NotFoundCoreException("CursoCompetencia no encontrado para el id: " + id);
        }
    }
}
