using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AzucareraPomalca.Infrastructure.Cores.Persistences
{
    public class CrudRepository<T, ID> : ICrudRepository<T, ID> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        protected CrudRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<IReadOnlyList<T>> FindAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> FindByIdAsync(ID id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> SaveAsync(T entity)
        {
            EntityState state = _dbContext.Entry(entity).State;

            _ = state switch
            {
                EntityState.Detached => _dbContext.Set<T>().Add(entity),
                EntityState.Modified => _dbContext.Set<T>().Update(entity)

            };

            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
