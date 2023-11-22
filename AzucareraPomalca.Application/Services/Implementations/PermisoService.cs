using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Permisos;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class PermisoService : IPermisoService
    {
        private readonly IPermisoRepository _permisoRepository;
        private readonly IMapper _mapper;

        public PermisoService(IPermisoRepository permisoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _permisoRepository = permisoRepository;
        }

        public Task<PermisoDto> CreateAsync(PermisoSaveDto saveDto)
        {
            throw new NotImplementedException();
        }

        public async Task<PermisoDto> EditAsync(int id, PermisoSaveDto saveDto)
        {
            Permiso? permiso = await _permisoRepository.FindByIdAsync(id);

            if (permiso is null) throw PermisoNotFound(id);

            _mapper.Map<PermisoSaveDto, Permiso>(saveDto, permiso);

            permiso.UpdatedAt = DateTime.UtcNow;

            await _permisoRepository.SaveAsync(permiso);

            return _mapper.Map<PermisoDto>(permiso);
        }

        public async Task<List<PermisoDto>> MenusByIdRolAsync(int id)
        {
            Expression<Func<Permiso, bool>> predicate = x => x.IdRol == id;

            List<Expression<Func<Permiso, object>>> includes = new List<Expression<Func<Permiso, object>>>()
            {
                t => t.Menu,
                t => t.Rol
            };

            var response = await _permisoRepository.FindAllAsync(predicate: predicate, includes: includes);

            return _mapper.Map<List<PermisoDto>>(response);
        }

        private NotFoundCoreException PermisoNotFound(int id)
        {
            return new NotFoundCoreException("Permiso no encontrada para el id: " + id);
        }
    }
}
