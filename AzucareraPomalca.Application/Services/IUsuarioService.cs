using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Profesiones;
using AzucareraPomalca.Application.Dtos.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Services
{
    public interface IUsuarioService : ICrudService<UsuarioDto, UsuarioSaveDto, int>, IPageService<UsuarioDto, UsuarioFilterDto>
    {
        Task<UsuarioSecurityDto> LoginAsync(UsuarioAuthDto userAuth);
    }
}
