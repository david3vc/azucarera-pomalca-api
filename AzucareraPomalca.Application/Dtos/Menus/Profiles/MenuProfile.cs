using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Menus.Profiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, MenuDto>();
        }
    }
}
