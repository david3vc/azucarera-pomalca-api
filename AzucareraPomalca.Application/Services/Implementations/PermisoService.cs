using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Menus;
using AzucareraPomalca.Application.Dtos.Permisos;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class PermisoService : IPermisoService
    {
        private readonly IPermisoRepository _permisoRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public PermisoService(IPermisoRepository permisoRepository, IMapper mapper, IMenuRepository menuRepository)
        {
            _mapper = mapper;
            _permisoRepository = permisoRepository;
            _menuRepository = menuRepository;
        }

        public async Task<PermisoDto> CreateAsync(PermisoSaveDto saveDto)
        {
            Expression<Func<Menu, bool>> predicate = x => x.Id == saveDto.IdMenu;
            Menu? menu = await _menuRepository.FindByIdAsync(predicate: predicate);
            if (menu is null) throw MenuNotFound(saveDto.IdMenu);

            Expression<Func<Menu, bool>> predicateMenuPadre = x => x.Id == menu.IdMenuPadre;
            Menu? menuPadre = await _menuRepository.FindByIdAsync(predicate: predicateMenuPadre);

            if (menuPadre is null) throw MenuNotFound((int)menu.IdMenuPadre);

            Expression<Func<Permiso, bool>> predicatePermiso = x => x.IdMenu == menuPadre.Id && x.IdRol == saveDto.IdRol;

            Permiso? permisoMenuPadre = await _permisoRepository.FindByIdAsync(predicate: predicatePermiso);

            if(permisoMenuPadre is null)
            {
                Permiso newPermisoMenuPadre = new Permiso()
                {
                    Consultar = true,
                    Editar = true,
                    Eliminar = true,
                    CreatedAt = DateTime.Now,
                    State = true,
                    IdMenu = menuPadre.Id,
                    IdRol = saveDto.IdRol
                };

                await _permisoRepository.SaveAsync(newPermisoMenuPadre);
            }

            Permiso permiso = _mapper.Map<Permiso>(saveDto);
            permiso.CreatedAt = DateTime.Now;
            permiso.State = true;

            await _permisoRepository.SaveAsync(permiso);

            return _mapper.Map<PermisoDto>(permiso);
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

        public async Task<List<PermisoDto>> MenusAsync(int id)
        {
            Expression<Func<Menu, bool>> predicate = x => x.Nivel == 2;

            IReadOnlyList<Menu> menus = await _menuRepository.FindAllAsync(predicate: predicate);

            Expression<Func<Permiso, bool>> predicatePermiso = x => x.IdRol == id && x.Menu.Nivel == 2;
            List<Expression<Func<Permiso, object>>> includes = new List<Expression<Func<Permiso, object>>>()
            {
                t => t.Menu,
                t => t.Rol
            };

            var permisos = await _permisoRepository.FindAllAsync(includes: includes, predicate: predicatePermiso);

            List<PermisoDto> response = new List<PermisoDto>();

            foreach(var menu in menus)
            {
                var flag = false;
                foreach(var permiso in permisos)
                {
                    if(menu.Id == permiso.IdMenu)
                    {
                        flag = true;
                        response.Add(_mapper.Map<PermisoDto>(permiso));
                    }
                }
                if (!flag)
                {
                    PermisoDto newPermiso = new PermisoDto()
                    {
                        IdMenu = menu.Id,
                        IdRol = id,
                        Consultar = false,
                        Editar = false,
                        Eliminar = false,
                        Menu = _mapper.Map<MenuDto>(menu)
                    };
                    response.Add(_mapper.Map<PermisoDto>(newPermiso));
                }
            }

            return response;
        }

        public async Task<List<PermisoDto>> MenusByIdRolAsync(int id)
        {
            Expression<Func<Permiso, bool>> predicate = x => x.IdRol == id;

            List<Expression<Func<Permiso, object>>> includes = new List<Expression<Func<Permiso, object>>>()
            {
                t => t.Menu,
                t => t.Rol
            };

            var response = await _permisoRepository.FindAllAsync(predicate: predicate, includes: includes, orderBy: x => x.OrderBy(t => t.Menu.Orden));

            var menusPadre = response.Where(t => t.Menu.Nivel == 1);
            var menusHijo = response.Where(t => t.Menu.Nivel == 2 && (t.Consultar == true || t.Editar == true || t.Eliminar == true));

            var menusReponse = new List<PermisoDto>();

            foreach(var menuPadre in menusPadre)
            {
                var menuResponse = new PermisoDto();
                menuResponse = _mapper.Map<PermisoDto>(menuPadre);
                menuResponse.Children = new List<PermisoDto>();
                foreach(var menuHijo in menusHijo)
                {
                    if(menuHijo.Menu.IdMenuPadre == menuPadre.Menu.Id)
                    {
                        menuResponse.Children.Add(_mapper.Map<PermisoDto>(menuHijo));
                    }
                }
                menusReponse.Add(menuResponse);
            }

            return menusReponse;
        }

        private NotFoundCoreException PermisoNotFound(int id)
        {
            return new NotFoundCoreException("Permiso no encontrada para el id: " + id);
        }

        private NotFoundCoreException MenuNotFound(int id)
        {
            return new NotFoundCoreException("Menu no encontrado para el id: " + id);
        }
    }
}
