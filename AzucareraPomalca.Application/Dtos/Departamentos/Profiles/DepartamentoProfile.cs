using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Divisiones;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Departamentos.Profiles
{
    public class DepartamentoProfile : Profile
    {
        public DepartamentoProfile()
        {
            CreateMap<Departamento, DepartamentoDto>();
            CreateMap<Departamento, DepartamentoSimpleDto>();
            CreateMap<Departamento, DepartamentoSaveDto>().ReverseMap();
            CreateMap<Departamento, DepartamentoFilterDto>().ReverseMap();
            CreateMap<PagedResult<Departamento>, PageResponse<DepartamentoDto>>();
        }
    }
}
