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
        Task<T> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllEagerLoadAsync(CancellationToken cancellationToken);
        Task<T> GetByIdEagerLoad(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken);
        T CreateAsync(T entity);
        bool CreateRangeAsync(List<T> entity);
        T UpdateAsync(T entity);
        bool UpdateRangeAsync(List<T> entity);
        bool DeleteAsync(T entity);
        bool DeleteRangeAsync(List<T> entity);
    }
}
