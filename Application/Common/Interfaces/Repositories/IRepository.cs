using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task<T> GetAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllEagerLoadAsync(CancellationToken cancellationToken);
        Task<T> GetByIdEagerLoad(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> CreateAsync(T entity, CancellationToken cancellationToken);
        Task<bool> CreateRangeAsync(List<T> entity, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<bool> UpdateRangeAsync(List<T> entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken);
        Task<bool> DeleteRangeAsync(List<T> entity, CancellationToken cancellationToken);
    }
}
