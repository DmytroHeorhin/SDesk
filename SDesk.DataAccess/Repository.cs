using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SDesc.DataAccess;
using SDesk.DataAccess.EF;

namespace SDesk.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        readonly SDeskContext _dbContext;
        readonly IDbSet<T> _dbSet;

        public Repository()
        {
            _dbContext = new SDeskContext();
            _dbSet = _dbContext.Set<T>();
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IList<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IList<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToArray();
        }

        public T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public T Attach(T entity)
        {
            return _dbSet.Attach(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
