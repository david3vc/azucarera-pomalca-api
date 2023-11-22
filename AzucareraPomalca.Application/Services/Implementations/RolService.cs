using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Roles;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;
        private readonly IMapper _mapper;

        public RolService(IRolRepository rolRepository, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
        }

        public async Task<RolDto> CreateAsync(RolSaveDto saveDto)
        {
            Rol rol = _mapper.Map<Rol>(saveDto);
            rol.CreatedAt = DateTime.UtcNow;
            rol.State = true;

            await _rolRepository.SaveAsync(rol);

            return _mapper.Map<RolDto>(rol);
        }

        public async Task<RolDto> DisabledAsync(int id)
        {
            Rol? rol = await _rolRepository.FindByIdAsync(id);

            if (rol is null) throw RolNotFound(id);

            rol.State = !rol.State;

            await _rolRepository.SaveAsync(rol);

            return _mapper.Map<RolDto>(rol);
        }

        public async Task<RolDto> EditAsync(int id, RolSaveDto saveDto)
        {
            Rol? rol = await _rolRepository.FindByIdAsync(id);

            if (rol is null) throw RolNotFound(id);

            _mapper.Map<RolSaveDto, Rol>(saveDto, rol);

            rol.UpdatedAt = DateTime.UtcNow;

            await _rolRepository.SaveAsync(rol);

            return _mapper.Map<RolDto>(rol);
        }

        public async Task<IReadOnlyList<RolDto>> FindAllAsync()
        {
            IReadOnlyList<Rol> roles = await _rolRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<RolDto>>(roles);
        }

        public async Task<PageResponse<RolDto>> FindAllPaginatedAsync(PageRequest<RolFilterDto> request)
        {
            var filter = request.Filter ?? new RolFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Rol, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Descripcion) || x.Descripcion.ToUpper().Contains(filter.Descripcion.ToUpper()))
                && (string.IsNullOrWhiteSpace(filter.Nombre) || x.Nombre.ToUpper().Contains(filter.Nombre.ToUpper()))
                && (!filter.State.HasValue || x.State == filter.State);

            var response = await _rolRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate);

            return _mapper.Map<PageResponse<RolDto>>(response);
        }

        public async Task<RolDto> FindByIdAsync(int id)
        {
            Rol? rol = await _rolRepository.FindByIdAsync(id);

            if (rol is null) throw RolNotFound(id);

            return _mapper.Map<RolDto>(rol);
        }

        private NotFoundCoreException RolNotFound(int id)
        {
            return new NotFoundCoreException("Rol no encontrado para el id: " + id);
        }
    }
}
