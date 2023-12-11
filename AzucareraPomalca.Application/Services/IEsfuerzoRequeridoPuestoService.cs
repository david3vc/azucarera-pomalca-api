using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.EsfuerzoRequeridoPuestos;

namespace AzucareraPomalca.Application.Services
{
    public interface IEsfuerzoRequeridoPuestoService : ICrudService<EsfuerzoRequeridoPuestoDto, EsfuerzoRequeridoPuestoSaveDto, int>
    {
    }
}
