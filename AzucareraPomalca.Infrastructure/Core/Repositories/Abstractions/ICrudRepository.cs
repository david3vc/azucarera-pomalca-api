namespace AzucareraPomalca.Infrastructure.Core.Repositories.Abstractions
{
    public interface ICrudRepository<T, ID>
    {
        Task<IReadOnlyList<T>> FindAllAsync();
        Task<T?> FindByIdAsync(ID id);
        Task<T> SaveAsync(T entity);
    }
}
