using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.TomaDecisionPuestos.Profiles
{
    public class TomaDecisionPuestoProfile : Profile
    {
        public TomaDecisionPuestoProfile()
        {
            CreateMap<TomaDecisionPuesto, TomaDecisionPuestoDto>();
            CreateMap<TomaDecisionPuesto, TomaDecisionPuestoSaveDto>().ReverseMap();
        }
    }
}
