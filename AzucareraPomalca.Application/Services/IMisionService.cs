using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Misiones;

namespace AzucareraPomalca.Application.Services
{
    public interface IMisionService : ICrudService<MisionDto, MisionSaveDto, int>
    {
    }
}
