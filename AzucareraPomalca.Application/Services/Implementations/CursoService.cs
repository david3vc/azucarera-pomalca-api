using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Cursos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IMapper _mapper;

        public CursoService(ICursoRepository cursoRepository, IMapper mapper)
        {
            _cursoRepository = cursoRepository;
            _mapper = mapper;
        }

        public async Task<CursoDto> CreateAsync(CursoSaveDto saveDto)
        {
            Curso curso = _mapper.Map<Curso>(saveDto);
            curso.CreatedAt = DateTime.UtcNow;
            curso.State = true;

            await _cursoRepository.SaveAsync(curso);

            return _mapper.Map<CursoDto>(curso);
        }

        public async Task<CursoDto> DisabledAsync(int id)
        {
            Curso? curso = await _cursoRepository.FindByIdAsync(id);

            if (curso is null) throw CursoNotFound(id);

            curso.State = !curso.State;

            await _cursoRepository.SaveAsync(curso);

            return _mapper.Map<CursoDto>(curso);
        }

        public async Task<CursoDto> EditAsync(int id, CursoSaveDto saveDto)
        {
            Curso? curso = await _cursoRepository.FindByIdAsync(id);

            if (curso is null) throw CursoNotFound(id);

            _mapper.Map<CursoSaveDto, Curso>(saveDto, curso);

            curso.UpdatedAt = DateTime.UtcNow;

            await _cursoRepository.SaveAsync(curso);

            return _mapper.Map<CursoDto>(curso);
        }

        public async Task<IReadOnlyList<CursoDto>> FindAllAsync()
        {
            IReadOnlyList<Curso> cursos = await _cursoRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<CursoDto>>(cursos);
        }

        public async Task<PageResponse<CursoDto>> FindAllPaginatedAsync(PageRequest<CursoFilterDto> request)
        {
            var filter = request.Filter ?? new CursoFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Curso, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Descripcion) || x.Descripcion.ToUpper().Contains(filter.Descripcion.ToUpper()))
                && (string.IsNullOrWhiteSpace(filter.Codigo) || x.Codigo.ToUpper().Contains(filter.Codigo.ToUpper()))
                && (string.IsNullOrWhiteSpace(filter.Gerencia) || x.Gerencia.ToUpper().Contains(filter.Gerencia.ToUpper()))
                && (!filter.State.HasValue || x.State == filter.State)
                && (!filter.IdTipoCurso.HasValue || x.IdTipoCurso == filter.IdTipoCurso);

            List<Expression<Func<Curso, object>>>? includes = new List<Expression<Func<Curso, object>>>()
            {
                t => t.TipoCurso
            };

            var response = await _cursoRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, includes: includes);

            return _mapper.Map<PageResponse<CursoDto>>(response);
        }

        public async Task<CursoDto> FindByIdAsync(int id)
        {
            List<Expression<Func<Curso, object>>>? includes = new List<Expression<Func<Curso, object>>>()
            {
                t => t.TipoCurso
            };
            Expression<Func<Curso, bool>> predicate = x => x.Id == id;

            Curso? curso = await _cursoRepository.FindByIdAsync(predicate: predicate, includes: includes);

            if (curso is null) throw CursoNotFound(id);

            return _mapper.Map<CursoDto>(curso);
        }

        private NotFoundCoreException CursoNotFound(int id)
        {
            return new NotFoundCoreException("Curso no encontrado para el id: " + id);
        }

        public async Task<PageResponse<CursoDuroSugeridoDto>> CursosDurosSugeridosPaginatedAsync(PageRequest<CursoDuroSugeridoFilterDto> request)
        {
            var filter = request.Filter != null ? _mapper.Map<CursoDuroSugerido>(request.Filter) : new CursoDuroSugerido();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            var response = await _cursoRepository.ListarCursosDurosSugeridosAsync(paging: paging, filter);

            return _mapper.Map<PageResponse<CursoDuroSugeridoDto>>(response);
        }
    }
}
