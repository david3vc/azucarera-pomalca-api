using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Gerencias;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class GerenciaService : IGerenciaService
    {
        private readonly IGerenciaRepository _gerenciaRepository;
        private readonly IMapper _mapper;

        public GerenciaService(IGerenciaRepository gerenciaRepository, IMapper mapper)
        {
            _gerenciaRepository = gerenciaRepository;
            _mapper = mapper;
        }

        public async Task<GerenciaDto> CreateAsync(GerenciaSaveDto saveDto)
        {
            Gerencia gerencia = _mapper.Map<Gerencia>(saveDto);
            gerencia.CreatedAt = DateTime.UtcNow;
            gerencia.State = true;

            await _gerenciaRepository.SaveAsync(gerencia);

            return _mapper.Map<GerenciaDto>(gerencia);
        }

        public async Task<GerenciaDto> DisabledAsync(int id)
        {
            Gerencia? gerencia = await _gerenciaRepository.FindByIdAsync(id);

            if (gerencia is null) throw GerenciaNotFound(id);

            gerencia.State = !gerencia.State;

            await _gerenciaRepository.SaveAsync(gerencia);

            return _mapper.Map<GerenciaDto>(gerencia);
        }

        public async Task<GerenciaDto> EditAsync(int id, GerenciaSaveDto saveDto)
        {
            Gerencia? gerencia = await _gerenciaRepository.FindByIdAsync(id);

            if (gerencia is null) throw GerenciaNotFound(id);

            _mapper.Map<GerenciaSaveDto, Gerencia>(saveDto, gerencia);

            gerencia.UpdatedAt = DateTime.UtcNow;

            await _gerenciaRepository.SaveAsync(gerencia);

            return _mapper.Map<GerenciaDto>(gerencia);
        }

        public async Task<IReadOnlyList<GerenciaDto>> FindAllAsync()
        {
            IReadOnlyList<Gerencia> gerencias = await _gerenciaRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<GerenciaDto>>(gerencias);
        }

        public async Task<PageResponse<GerenciaDto>> FindAllPaginatedAsync(PageRequest<GerenciaFilterDto> request)
        {
            var filter = request.Filter ?? new GerenciaFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Gerencia, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Nombre) || x.Nombre.ToUpper().Contains(filter.Nombre.ToUpper()))
                && (!filter.State.HasValue || x.State == filter.State);

            var response = await _gerenciaRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate);

            return _mapper.Map<PageResponse<GerenciaDto>>(response);
        }

        public async Task<GerenciaDto> FindByIdAsync(int id)
        {
            Gerencia? gerencia = await _gerenciaRepository.FindByIdAsync(id);

            if (gerencia is null) throw GerenciaNotFound(id);

            return _mapper.Map<GerenciaDto>(gerencia);
        }

        public async Task<IReadOnlyList<GerenciaSimpleDto>> SimpleListAsync()
        {
            IReadOnlyList<Gerencia> gerencias = await _gerenciaRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<GerenciaSimpleDto>>(gerencias);
        }

        private NotFoundCoreException GerenciaNotFound(int id)
        {
            return new NotFoundCoreException("Gerencia no encontrada para el id: " + id);
        }
    }
}
