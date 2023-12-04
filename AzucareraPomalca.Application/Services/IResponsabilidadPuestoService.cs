using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.ResponsabilidadesPuestos;

namespace AzucareraPomalca.Application.Services
{
    public interface IResponsabilidadPuestoService : ICrudService<ResponsabilidadPuestoDto, ResponsabilidadPuestoSaveDto, int>
    {
    }
}
