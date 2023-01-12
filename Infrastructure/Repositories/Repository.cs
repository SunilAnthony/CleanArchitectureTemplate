using Application.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _entities;
        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public virtual T CreateAsync(T entity)
        {
            try
            {
                _entities.Add(entity);
                return entity;
            }
            catch (Exception)
            {

                return new T();
            }
        }

        public virtual bool DeleteAsync(T entity)
        {
            try
            {
                _entities.Remove(entity);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation)
        {
            return await _entities.Where(predicate).ToListAsync(cancellation);
        }

        public virtual async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _entities.ToListAsync(cancellationToken);
        }

        public virtual async Task<T> GetAsync(int StudentId, CancellationToken cancellationToken = default)
        {
            return await _entities.FindAsync(StudentId, cancellationToken) ?? new T();
        }

        public virtual T UpdateAsync(T entity)
        {
            try
            {
                _entities.Update(entity);
                return entity;
            }
            catch (Exception)
            {

                return new T();
            }
        }

        public async virtual Task<IEnumerable<T>> GetAllEagerLoadAsync(CancellationToken cancellationToken = default)
        {
            var query = _entities.AsQueryable();


            foreach (var property in _context.Model.FindEntityType(typeof(T)).GetNavigations())
            {
                query = query.Include(property.Name);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async virtual Task<T> GetByIdEagerLoad(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var query = _entities.Where(predicate).AsQueryable();

            foreach (var property in _context.Model.FindEntityType(typeof(T))!.GetNavigations())
            {
                query = query.Include(property.Name);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual bool CreateRangeAsync(List<T> entity)
        {
            try
            {
                _entities.AddRange(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual bool UpdateRangeAsync(List<T> entity)
        {
            try
            {
                _entities.UpdateRange(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public virtual bool DeleteRangeAsync(List<T> entity)
        {
            try
            {
                _entities.RemoveRange(entity);
                return true;
            }
            catch (Exception)
            {
               return false;
            }
        }
    }
}

