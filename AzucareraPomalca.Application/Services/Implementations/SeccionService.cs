using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Departamentos;
using AzucareraPomalca.Application.Dtos.Secciones;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class SeccionService : ISeccionService
    {
        private readonly ISeccionRepository _seccionRepository;
        private readonly IMapper _mapper;

        public SeccionService(ISeccionRepository seccionRepository, IMapper mapper)
        {
            _seccionRepository = seccionRepository;
            _mapper = mapper;
        }

        public async Task<SeccionDto> CreateAsync(SeccionSaveDto saveDto)
        {
            Seccion seccion = _mapper.Map<Seccion>(saveDto);
            seccion.CreatedAt = DateTime.UtcNow;
            seccion.State = true;

            await _seccionRepository.SaveAsync(seccion);

            return _mapper.Map<SeccionDto>(seccion);
        }

        public async Task<SeccionDto> DisabledAsync(int id)
        {
            Seccion? seccion = await _seccionRepository.FindByIdAsync(id);

            if (seccion is null) throw SeccionNotFound(id);

            seccion.State = !seccion.State;

            await _seccionRepository.SaveAsync(seccion);

            return _mapper.Map<SeccionDto>(seccion);
        }

        public async Task<SeccionDto> EditAsync(int id, SeccionSaveDto saveDto)
        {
            Seccion? seccion = await _seccionRepository.FindByIdAsync(id);

            if (seccion is null) throw SeccionNotFound(id);

            _mapper.Map<SeccionSaveDto, Seccion>(saveDto, seccion);

            seccion.UpdatedAt = DateTime.UtcNow;

            await _seccionRepository.SaveAsync(seccion);

            return _mapper.Map<SeccionDto>(seccion);
        }

        public async Task<IReadOnlyList<SeccionDto>> FindAllAsync()
        {
            IReadOnlyList<Seccion> secciones = await _seccionRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<SeccionDto>>(secciones);
        }

        public async Task<PageResponse<SeccionDto>> FindAllPaginatedAsync(PageRequest<SeccionFilterDto> request)
        {
            var filter = request.Filter ?? new SeccionFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Seccion, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Nombre) || x.Nombre.ToUpper().Contains(filter.Nombre.ToUpper()))
                && (!filter.IdGerencia.HasValue || x.IdGerencia == filter.IdGerencia)
                && (!filter.IdDivision.HasValue || x.IdDivision == filter.IdDivision)
                && (!filter.IdDepartamento.HasValue || x.IdDepartamento == filter.IdDepartamento)
                && (!filter.State.HasValue || x.State == filter.State);

            List<Expression<Func<Seccion, object>>> includes = new List<Expression<Func<Seccion, object>>>()
            {
                t => t.Gerencia,
                t => t.Division,
                t => t.Departamento
            };

            var response = await _seccionRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, includes: includes);

            return _mapper.Map<PageResponse<SeccionDto>>(response);
        }

        public async Task<SeccionDto> FindByIdAsync(int id)
        {
            Seccion? seccion = await _seccionRepository.FindByIdAsync(id);

            if (seccion is null) throw SeccionNotFound(id);

            return _mapper.Map<SeccionDto>(seccion);
        }

        public async Task<IReadOnlyList<SeccionSimpleDto>> SimpleListAsync()
        {
            IReadOnlyList<Seccion> secciones = await _seccionRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<SeccionSimpleDto>>(secciones);
        }

        public async Task<IReadOnlyList<SeccionSimpleDto>> SimpleListByIdsAsync(SeccionSimpleFilterDto request)
        {
            Expression<Func<Seccion, bool>>? predicate = x => (!request.IdGerencia.HasValue || x.IdGerencia == request.IdGerencia)
                                                               && (!request.IdDivision.HasValue || x.IdDivision == request.IdDivision)
                                                               && (!request.IdDepartamento.HasValue || x.IdDepartamento == request.IdDepartamento);

            IReadOnlyList<Seccion> claseOcupacionales = await _seccionRepository.FindAllAsync(predicate: predicate);

            return _mapper.Map<IReadOnlyList<SeccionSimpleDto>>(claseOcupacionales);
        }

        private NotFoundCoreException SeccionNotFound(int id)
        {
            return new NotFoundCoreException("Seccion no encontrado para el id: " + id);
        }
    }
}
