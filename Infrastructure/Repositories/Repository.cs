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
        public async virtual Task<bool> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                _entities.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async virtual Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                _entities.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _entities.ToListAsync(cancellationToken);
        }

        public virtual async Task<T> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _entities.FindAsync(id, cancellationToken) ?? new T();
        }

        public async virtual Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                _entities.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {

                return false;
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

            foreach (var property in _context.Model.FindEntityType(typeof(T)).GetNavigations())
            {
                query = query.Include(property.Name);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async virtual Task<bool> CreateRangeAsync(List<T> entity, CancellationToken cancellationToken = default)
        {
            try
            {
                _entities.AddRange(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async virtual Task<bool> UpdateRangeAsync(List<T> entity, CancellationToken cancellationToken = default)
        {
            try
            {
                _entities.UpdateRange(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async virtual Task<bool> DeleteRangeAsync(List<T> entity, CancellationToken cancellationToken = default)
        {
            try
            {
                _entities.RemoveRange(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}

