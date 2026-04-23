using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TrackingApp.Core.Entites;

namespace TrackingApp.Repository.Abstract.Repositories
{
    public abstract class GenericRepository<T> where T : BaseEntity, new()
    {

        protected readonly AppContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppContext context)
        {
            _context = context;

            _dbSet = _context.Set<T>();
        }

          public void Add(T entity) => _dbSet.Add(entity);
          public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
          public async Task AddRange(IEnumerable<T> entities) => await _dbSet.AddRangeAsync(entities);
          public void Remove(T entity) => _dbSet.Remove(entity);
          public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);
          public async Task<IEnumerable<T>> GetAll() =>  await _dbSet.ToListAsync();
          public async Task<T> GetById(int id) => await _dbSet.FindAsync(id);
          public IQueryable<T> Where(Expression<Func<T, bool>> expression) => _dbSet.Where(expression);

    }
}
