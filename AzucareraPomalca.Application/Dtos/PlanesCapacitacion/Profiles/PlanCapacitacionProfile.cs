using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.PlanesCapacitacion.Profiles
{
    public class PlanCapacitacionProfile : Profile
    {
        public PlanCapacitacionProfile()
        {
            CreateMap<PlanCapacitacion, PlanCapacitacionDto>()
                // Resumen se calcula en el service, no se mapea desde la entidad.
                .ForMember(dest => dest.Resumen, opt => opt.Ignore());
            CreateMap<PlanCapacitacion, PlanCapacitacionSaveDto>().ReverseMap();
            CreateMap<PlanCapacitacion, PlanCapacitacionFilterDto>().ReverseMap();

            CreateMap<PagedResult<PlanCapacitacion>, PageResponse<PlanCapacitacionDto>>();
        }
    }
}
