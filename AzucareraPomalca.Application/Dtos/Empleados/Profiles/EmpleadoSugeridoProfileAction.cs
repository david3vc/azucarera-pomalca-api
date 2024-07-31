using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Empleados.Profiles
{
    public class EmpleadoSugeridoProfileAction : IMappingAction<PagedResult<EmpleadoSugerido>, PageResponse<EmpleadoSugeridoDto>>
    {
        public void Process(PagedResult<EmpleadoSugerido> source, PageResponse<EmpleadoSugeridoDto> destination, ResolutionContext context)
        {
            for (int i = 0; i < destination.Data.Count; i++)
            {
                destination.Data[i].NombreCompleto = $"{source.Data[i].Nombre} {source.Data[i].ApellidoPaterno} {source.Data[i].ApellidoMaterno}";
            }
        }
    }
}
