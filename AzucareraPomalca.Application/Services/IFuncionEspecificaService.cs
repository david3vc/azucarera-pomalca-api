using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.FuncionesEspecificas;

namespace AzucareraPomalca.Application.Services
{
    public interface IFuncionEspecificaService : ICrudService<FuncionEspecificaDto, FuncionEspecificaSaveDto, int>
    {
    }
}
