using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TrackingApp.Core.Entites;

namespace TrackingApp.Core.Abstract.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        public void Add(T entity);
        public Task AddAsync(T entity);
        public Task AddRange(IEnumerable<T> entities);
        public void Remove(T entity);
        public void RemoveRange(IEnumerable<T> entities);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
        public IQueryable<T> Where(Expression<Func<T, bool>> expression);

       
    }   
}

