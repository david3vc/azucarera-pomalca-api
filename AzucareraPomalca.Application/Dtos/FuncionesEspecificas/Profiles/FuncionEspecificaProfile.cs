using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.FuncionesEspecificas.Profiles
{
    public class FuncionEspecificaProfile : Profile
    {
        public FuncionEspecificaProfile()
        {
            CreateMap<FuncionEspecifica, FuncionEspecificaDto>();
            CreateMap<FuncionEspecifica, FuncionEspecificaSaveDto>().ReverseMap();
        }
    }
}
