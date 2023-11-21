namespace AzucareraPomalca.Application.Cores.Services
{
    public interface IListService<TDto>
    {
        Task<IReadOnlyList<TDto>> SimpleListAsync();
    }
}
