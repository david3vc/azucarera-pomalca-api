using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Roles;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.Misiones.Profiles
{
    public class MisionProfile : Profile
    {
        public MisionProfile()
        {
            CreateMap<Mision, MisionDto>();
            CreateMap<Mision, MisionSaveDto>().ReverseMap();
        }
    }
}
