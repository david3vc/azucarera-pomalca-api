using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Departamentos;
using AzucareraPomalca.Application.Dtos.Divisiones;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IMapper _mapper;

        public DepartamentoService(IDepartamentoRepository departamentoRepository, IMapper mapper)
        {
            _departamentoRepository = departamentoRepository;
            _mapper = mapper;
        }

        public async Task<DepartamentoDto> CreateAsync(DepartamentoSaveDto saveDto)
        {
            Departamento departamento = _mapper.Map<Departamento>(saveDto);
            departamento.CreatedAt = DateTime.UtcNow;
            departamento.State = true;

            await _departamentoRepository.SaveAsync(departamento);

            return _mapper.Map<DepartamentoDto>(departamento);
        }

        public async Task<DepartamentoDto> DisabledAsync(int id)
        {
            Departamento? departamento = await _departamentoRepository.FindByIdAsync(id);

            if (departamento is null) throw DepartamentoNotFound(id);

            departamento.State = !departamento.State;

            await _departamentoRepository.SaveAsync(departamento);

            return _mapper.Map<DepartamentoDto>(departamento);
        }

        public async Task<DepartamentoDto> EditAsync(int id, DepartamentoSaveDto saveDto)
        {
            Departamento? departamento = await _departamentoRepository.FindByIdAsync(id);

            if (departamento is null) throw DepartamentoNotFound(id);

            _mapper.Map<DepartamentoSaveDto, Departamento>(saveDto, departamento);

            departamento.UpdatedAt = DateTime.UtcNow;

            await _departamentoRepository.SaveAsync(departamento);

            return _mapper.Map<DepartamentoDto>(departamento);
        }

        public async Task<IReadOnlyList<DepartamentoDto>> FindAllAsync()
        {
            IReadOnlyList<Departamento> departamentos = await _departamentoRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<DepartamentoDto>>(departamentos);
        }

        public async Task<PageResponse<DepartamentoDto>> FindAllPaginatedAsync(PageRequest<DepartamentoFilterDto> request)
        {
            var filter = request.Filter ?? new DepartamentoFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Departamento, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Nombre) || x.Nombre.ToUpper().Contains(filter.Nombre.ToUpper()))
                && (!filter.IdGerencia.HasValue || x.IdGerencia == filter.IdGerencia)
                && (!filter.IdDivision.HasValue || x.IdDivision == filter.IdDivision)
                && (!filter.State.HasValue || x.State == filter.State);

            List<Expression<Func<Departamento, object>>> includes = new List<Expression<Func<Departamento, object>>>()
            {
                t => t.Gerencia,
                t => t.Division
            };

            var response = await _departamentoRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, includes: includes);

            return _mapper.Map<PageResponse<DepartamentoDto>>(response);
        }

        public async Task<DepartamentoDto> FindByIdAsync(int id)
        {
            Departamento? departamento = await _departamentoRepository.FindByIdAsync(id);

            if (departamento is null) throw DepartamentoNotFound(id);

            return _mapper.Map<DepartamentoDto>(departamento);
        }

        public async Task<IReadOnlyList<DepartamentoSimpleDto>> SimpleListAsync()
        {
            IReadOnlyList<Departamento> departamentos = await _departamentoRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<DepartamentoSimpleDto>>(departamentos);
        }

        public async Task<IReadOnlyList<DepartamentoSimpleDto>> SimpleListByIdsAsync(DepartamentoSimpleFilterDto request)
        {
            Expression<Func<Departamento, bool>>? predicate = x => (!request.IdGerencia.HasValue || x.IdGerencia == request.IdGerencia)
                                                                && (!request.IdDivision.HasValue || x.IdDivision == request.IdDivision);

            IReadOnlyList<Departamento> claseOcupacionales = await _departamentoRepository.FindAllAsync(predicate: predicate);

            return _mapper.Map<IReadOnlyList<DepartamentoSimpleDto>>(claseOcupacionales);
        }

        private NotFoundCoreException DepartamentoNotFound(int id)
        {
            return new NotFoundCoreException("Departamento no encontrado para el id: " + id);
        }
    }
}
