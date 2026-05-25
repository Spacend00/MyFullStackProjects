
using Microsoft.EntityFrameworkCore.Query;
using PostAppAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace PostAppAPI.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<T?> GetAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool withTracking = true);
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool withTracking = false);
        Task<T> CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
