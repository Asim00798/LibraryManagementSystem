#region Usings
using LibraryManagement.Domain.Interfaces;
using LibraryManagementSystem.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
#endregion
namespace LibraryManagementSystem.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly LibraryDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(LibraryDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        #region Read Methods
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
                                                  params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.Where(predicate).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntity>> FindAsync() =>
            await _dbSet.ToListAsync();
        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            foreach (var include in includes)
                query = query.Include(include);
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            foreach (var include in includes)
                query = query.Include(include);
            return await query.ToListAsync();
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbSet.FirstOrDefaultAsync(predicate);

        public async Task<TEntity?> FirstOrDefaultAsync() =>
            await _dbSet.FirstOrDefaultAsync();

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbSet.AnyAsync(predicate);

        public async Task<bool> AnyAsync() =>
            await _dbSet.AnyAsync();

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null) =>
            predicate == null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(predicate);

        public async Task<int> CountAsync() =>
            await _dbSet.CountAsync();

        public IQueryable<TEntity> Query() => _dbSet.AsQueryable();

        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return query;
        }

        #endregion

        #region Write Methods

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return await Task.FromResult(entity);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        #endregion
    }
}
