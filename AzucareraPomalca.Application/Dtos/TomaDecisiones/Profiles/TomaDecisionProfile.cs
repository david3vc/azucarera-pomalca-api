using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.TomaDecisiones.Profiles
{
    public class TomaDecisionProfile : Profile
    {
        public TomaDecisionProfile()
        {
            CreateMap<TomaDecision, TomaDecisionDto>();
        }
    }
}
