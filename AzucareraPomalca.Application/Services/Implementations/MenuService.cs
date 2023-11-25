using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Menus;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<MenuDto>> FindAllAsync()
        {
            Expression<Func<Menu, bool>> predicate = x => x.Nivel == 2;

            IReadOnlyList<Menu> menus = await _menuRepository.FindAllAsync(predicate: predicate);

            return _mapper.Map<IReadOnlyList<MenuDto>>(menus);
        }

        public async Task<MenuDto> FindByIdAsync(int id)
        {
            Menu? menu = await _menuRepository.FindByIdAsync(id);

            if (menu is null) throw MenuNotFound(id);

            return _mapper.Map<MenuDto>(menu);
        }

        private NotFoundCoreException MenuNotFound(int id)
        {
            return new NotFoundCoreException("Menu no encontrado para el id: " + id);
        }
    }
}
