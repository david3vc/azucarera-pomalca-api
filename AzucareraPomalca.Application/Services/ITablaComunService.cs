using AzucareraPomalca.Application.Dtos.TablaComunes;

namespace AzucareraPomalca.Application.Services
{
    public interface ITablaComunService
    {
        Task<IReadOnlyList<TablaComunDto>> FindAllByIdsAsync(TablaComunFilterDto filter);
    }
}
