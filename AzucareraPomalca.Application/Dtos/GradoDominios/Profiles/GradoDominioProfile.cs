using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Cursos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.GradoDominios.Profiles
{
    public class GradoDominioProfile : Profile
    {
        public GradoDominioProfile()
        {
            CreateMap<GradoDominio, GradoDominioDto>();
            CreateMap<GradoDominio, GradoDominioSaveDto>().ReverseMap();
            CreateMap<GradoDominio, GradoDominioFilterDto>().ReverseMap();

            CreateMap<PagedResult<GradoDominio>, PageResponse<GradoDominioDto>>();
        }
    }
}
