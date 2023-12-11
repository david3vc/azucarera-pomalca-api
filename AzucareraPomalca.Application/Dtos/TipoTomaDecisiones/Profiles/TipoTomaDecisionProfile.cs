using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.TipoTomaDecisiones.Profiles
{
    public class TipoTomaDecisionProfile : Profile
    {
        public TipoTomaDecisionProfile()
        {
            CreateMap<TipoTomaDecision, TipoTomaDecisionDto>();
        }
    }
}
