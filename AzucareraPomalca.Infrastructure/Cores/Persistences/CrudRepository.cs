using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

            if (state == EntityState.Detached)
            {
                _dbContext.Set<T>().Add(entity);
            }
            else if (state == EntityState.Modified)
            {
                _dbContext.Set<T>().Update(entity);
            }
            else
            {
                _dbContext.Set<T>().Update(entity);

            }


            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _dbContext.Set<T>().Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return entity;
        }

        public async Task<IReadOnlyList<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> FindAllAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object>>>? includes = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();


            return await query.ToListAsync();
        }

        public async Task<T?> FindByIdAsync(
            Expression<Func<T, bool>> predicate,
            List<Expression<Func<T, object>>>? includes = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task<T?> FindFirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            List<Expression<Func<T, object>>>? includes = null,
                                       bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.Where(predicate).FirstOrDefaultAsync();
        }


        public virtual async Task<PagedResult<T>> FindAllPaginatedAsync(Paging pagin,
            Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(predicate);

            return await GetPagedResult(query, pagin);
        }

        public virtual async Task<PagedResult<T>> FindAllPaginatedAsync(Paging pagin,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object>>>? includes = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) query = orderBy(query).AsQueryable();

            return await GetPagedResult(query, pagin);
        }

        protected async Task<PagedResult<T>> GetPagedResult(IQueryable<T> query, Paging pagin)
        {
            var totalElements = await query.CountAsync();
            var skip = (pagin.PageNumber - 1) * pagin.PageSize;
            var data = await query.Skip(skip)
                .Take(pagin.PageSize)
                .ToListAsync();

            return new PagedResult<T>(data, pagin, totalElements);
        }
    }
}
