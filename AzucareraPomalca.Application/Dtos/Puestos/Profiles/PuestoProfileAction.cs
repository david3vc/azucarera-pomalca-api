using AutoMapper;
using AzucareraPomalca.Application.Dtos.Coordinaciones;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Puestos.Profiles
{
    public class PuestoProfileAction : IMappingAction<Puesto, PuestoDto>
    {
        public void Process(Puesto source, PuestoDto destination, ResolutionContext context)
        {
            if (source.CoordinacionesPuestoCoordinado != null && source.CoordinacionesPuestoCoordinado.Count > 0)
            {
                List<CoordinacionDto> mismaGerencia = new List<CoordinacionDto>();
                List<CoordinacionDto> otraGerencia = new List<CoordinacionDto>();

                foreach (var coordinacion in source.CoordinacionesPuestoCoordinado)
                {
                    if (coordinacion.PuestoCoordinado.IdGerencia == source.IdGerencia)
                    {
                        mismaGerencia.Add(context.Mapper.Map<CoordinacionDto>(coordinacion));
                    }
                    else
                    {
                        otraGerencia.Add(context.Mapper.Map<CoordinacionDto>(coordinacion));
                    }
                }

                destination.CoordinacionesMismaGerencia = mismaGerencia;
                destination.CoordinacionesOtraGerencia = otraGerencia;
            }
        }
    }
}
