using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Competencias;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class CompetenciaService : ICompetenciaService
    {
        private readonly IMapper _mapper;
        private readonly ICompetenciaRepository _competenciaRepository;
        private readonly IGradoDominioService _gradoDominioService;

        public CompetenciaService(ICompetenciaRepository competenciaRepository, IMapper mapper, IGradoDominioService gradoDominioService)
        {
            _competenciaRepository = competenciaRepository;
            _mapper = mapper;
            _gradoDominioService = gradoDominioService;
        }

        public async Task<CompetenciaDto> CreateAsync(CompetenciaSaveDto saveDto)
        {
            Competencia competencia = _mapper.Map<Competencia>(saveDto);
            competencia.CreatedAt = DateTime.UtcNow;
            competencia.State = true;

            await _competenciaRepository.SaveAsync(competencia);

            foreach (var gradoDominio in saveDto.GradosDominioSave)
            {
                gradoDominio.IdCompetencia = competencia.Id;
                await _gradoDominioService.CreateAsync(gradoDominio);
            }

            return _mapper.Map<CompetenciaDto>(competencia);
        }

        public async Task<CompetenciaDto> DisabledAsync(int id)
        {
            Competencia? competencia = await _competenciaRepository.FindByIdAsync(id);

            if (competencia is null) throw CompetenciaNotFound(id);

            competencia.State = !competencia.State;

            await _competenciaRepository.SaveAsync(competencia);

            return _mapper.Map<CompetenciaDto>(competencia);
        }

        public async Task<CompetenciaDto> EditAsync(int id, CompetenciaSaveDto saveDto)
        {
            Competencia? competencia = await _competenciaRepository.FindByIdAsync(id);

            if (competencia is null) throw CompetenciaNotFound(id);

            _mapper.Map<CompetenciaSaveDto, Competencia>(saveDto, competencia);

            competencia.UpdatedAt = DateTime.UtcNow;

            await _competenciaRepository.SaveAsync(competencia);

            foreach (var gradoDominio in saveDto.GradosDominioSave)
            {
                gradoDominio.IdCompetencia = competencia.Id;
                if (gradoDominio.Id != 0 && gradoDominio.Id != null)
                    await _gradoDominioService.EditAsync(gradoDominio.Id ?? 0, gradoDominio);
                else
                    await _gradoDominioService.CreateAsync(gradoDominio);
            }

            return _mapper.Map<CompetenciaDto>(competencia);
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
                && (!filter.IdTipoCompetencia.HasValue || x.IdTipoCompetencia == filter.IdTipoCompetencia)
                && (!filter.State.HasValue || x.State == filter.State);

            List<Expression<Func<Competencia, object>>>? includes = new List<Expression<Func<Competencia, object>>>()
            {
                t => t.TipoCompetencia,
                t => t.GradoDominios.Where(t => t.State == true)
            };

            var response = await _competenciaRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, includes: includes);

            return _mapper.Map<PageResponse<CompetenciaDto>>(response);
        }

        public async Task<CompetenciaDto> FindByIdAsync(int id)
        {
            List<Expression<Func<Competencia, object>>>? includes = new List<Expression<Func<Competencia, object>>>()
            {
                t => t.TipoCompetencia,
                t => t.GradoDominios
            };
            Expression<Func<Competencia, bool>> predicate = x => x.Id == id;

            Competencia? curso = await _competenciaRepository.FindByIdAsync(predicate: predicate, includes: includes);

            if (curso is null) throw CompetenciaNotFound(id);

            return _mapper.Map<CompetenciaDto>(curso);
        }

        private NotFoundCoreException CompetenciaNotFound(int id)
        {
            return new NotFoundCoreException("Competencia no encontrado para el id: " + id);
        }
    }
}
