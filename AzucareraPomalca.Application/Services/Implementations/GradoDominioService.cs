using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.GradoDominios;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class GradoDominioService : IGradoDominioService
    {
        private readonly IMapper _mapper;
        private readonly IGradoDominioRepository _gradoDominioRepository;

        public GradoDominioService(IMapper mapper, IGradoDominioRepository gradoDominioRepository)
        {
            _mapper = mapper;
            _gradoDominioRepository = gradoDominioRepository;
        }

        public async Task<GradoDominioDto> CreateAsync(GradoDominioSaveDto saveDto)
        {
            GradoDominio gradoDominio = _mapper.Map<GradoDominio>(saveDto);
            gradoDominio.CreatedAt = DateTime.UtcNow;
            gradoDominio.State = true;

            await _gradoDominioRepository.SaveAsync(gradoDominio);

            return _mapper.Map<GradoDominioDto>(gradoDominio);
        }

        public async Task<GradoDominioDto> DisabledAsync(int id)
        {
            GradoDominio? gradoDominio = await _gradoDominioRepository.FindByIdAsync(id);

            if (gradoDominio is null) throw GradoDominioByIdNotFound(id);

            gradoDominio.State = !gradoDominio.State;

            await _gradoDominioRepository.SaveAsync(gradoDominio);

            return _mapper.Map<GradoDominioDto>(gradoDominio);
        }

        public async Task<GradoDominioDto> EditAsync(int id, GradoDominioSaveDto saveDto)
        {
            GradoDominio? gradoDominio = await _gradoDominioRepository.FindByIdAsync(id);

            if (gradoDominio is null) throw GradoDominioByIdNotFound(id);

            _mapper.Map<GradoDominioSaveDto, GradoDominio>(saveDto, gradoDominio);

            gradoDominio.UpdatedAt = DateTime.UtcNow;

            await _gradoDominioRepository.SaveAsync(gradoDominio);

            return _mapper.Map<GradoDominioDto>(gradoDominio);
        }

        public Task<IReadOnlyList<GradoDominioDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PageResponse<GradoDominioDto>> FindAllPaginatedAsync(PageRequest<GradoDominioFilterDto> request)
        {
            var filter = request.Filter ?? new GradoDominioFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<GradoDominio, bool>> predicate = x =>
                (!filter.Nivel.HasValue || x.Nivel == filter.Nivel)
                && (!filter.IdCompetencia.HasValue || x.IdCompetencia == filter.IdCompetencia);

            var response = await _gradoDominioRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate);

            return _mapper.Map<PageResponse<GradoDominioDto>>(response);
        }

        public async Task<GradoDominioDto> FindByIdAsync(int id)
        {
            GradoDominio? curso = await _gradoDominioRepository.FindByIdAsync(id);

            if (curso is null) throw GradoDominioByIdNotFound(id);

            return _mapper.Map<GradoDominioDto>(curso);
        }

        public async Task<GradoDominioDto> FindByNivelAndIdCompetenciaAsync(GradoDominioFilterDto request)
        {
            Expression<Func<GradoDominio, bool>> predicate = x => x.Nivel == request.Nivel && x.IdCompetencia == request.IdCompetencia;

            List<Expression<Func<GradoDominio, object>>>? includes = new List<Expression<Func<GradoDominio, object>>>()
            {
                t => t.CompetenciaSimple.TipoCompetencia
            };

            GradoDominio? gradoDominio = await _gradoDominioRepository.FindByIdAsync(predicate: predicate, includes: includes);

            if (gradoDominio is null) throw GradoDominioNotFound(request);

            return _mapper.Map<GradoDominioDto>(gradoDominio);
        }

        private NotFoundCoreException GradoDominioByIdNotFound(int id)
        {
            return new NotFoundCoreException("Grado Dominio no encontrado para el id: " + id);
        }

        private NotFoundCoreException GradoDominioNotFound(GradoDominioFilterDto request)
        {
            return new NotFoundCoreException("Grado Dominio no encontrado para el nivel: " + request.Nivel + " e id_competencia: idCompetencia: " + request.IdCompetencia);
        }
    }
}
