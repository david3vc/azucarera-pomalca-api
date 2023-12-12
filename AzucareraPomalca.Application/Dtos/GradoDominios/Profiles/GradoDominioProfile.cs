using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.GradoDominios.Profiles
{
    public class GradoDominioProfile : Profile
    {
        public GradoDominioProfile()
        {
            CreateMap<GradoDominio, GradoDominioDto>();
        }
    }
}
