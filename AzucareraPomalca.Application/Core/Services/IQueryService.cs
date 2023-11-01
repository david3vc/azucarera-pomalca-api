namespace AzucareraPomalca.Application.Core.Services
{
    public interface IQueryService<TDto, ID>
    {
        Task<IReadOnlyList<TDto>> FindAllAsync();
        Task<TDto> FindByIdAsync(ID id);
    }
}
