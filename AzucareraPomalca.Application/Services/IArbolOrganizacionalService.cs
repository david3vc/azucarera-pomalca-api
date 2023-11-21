using AzucareraPomalca.Application.Dtos.ArbolOrganizacional;

namespace AzucareraPomalca.Application.Services
{
    public interface IArbolOrganizacionalService
    {
        Task<NodoDto> FindNodoPrincipalPadre();
        Task<NodoDto> FindNodoUnidadOrganizacionByIdUnidadOrganizacionalAsync(NodoFilterDto filter);
        Task<List<NodoDto>> FindNodoPuestoById(int id);
    }
}
