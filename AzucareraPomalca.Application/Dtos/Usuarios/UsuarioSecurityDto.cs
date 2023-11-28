using AzucareraPomalca.Core.Securities.Etities;

namespace AzucareraPomalca.Application.Dtos.Usuarios
{
    public class UsuarioSecurityDto : UsuarioDto
    {
        public SecurityEntity? Security { get; set; }
    }
}
