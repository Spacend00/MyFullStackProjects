
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PostAppAPI.Application.Interfaces;
using PostAppAPI.Domain.Interfaces;
using PostAppAPI.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace PostAppAPI.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly PostAppAPIDbContext _context;

        public GenericRepository(PostAppAPIDbContext context)
        {
            _context = context;
        }
        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool withTracking = false)
        {
            IQueryable<T> query = _context.Set<T>();
            if (!withTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            return await query.ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool withTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (!withTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);
            var result = await query.FirstOrDefaultAsync(predicate);
            return result;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
