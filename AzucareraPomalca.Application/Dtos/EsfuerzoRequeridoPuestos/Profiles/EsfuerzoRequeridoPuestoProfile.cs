using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.EsfuerzoRequeridoPuestos.Profiles
{
    public class EsfuerzoRequeridoPuestoProfile : Profile
    {
        public EsfuerzoRequeridoPuestoProfile()
        {
            CreateMap<EsfuerzoRequeridoPuesto, EsfuerzoRequeridoPuestoDto>();
            CreateMap<EsfuerzoRequeridoPuesto, EsfuerzoRequeridoPuestoSaveDto>().ReverseMap();
        }
    }
}
