using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Usuarios;

namespace AzucareraPomalca.Application.Services
{
    public interface IUsuarioService : ICrudService<UsuarioDto, UsuarioSaveDto, int>, IPageService<UsuarioDto, UsuarioFilterDto>
    {
        Task<UsuarioSecurityDto> LoginAsync(UsuarioAuthDto userAuth);
    }
}
