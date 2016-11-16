using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SDesc.DataAccess
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T Get(int id);
        IList<T> GetAll();
        IList<T> SearchFor(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Attach(T enttity);
        void Update(T entity);
        void Delete(int id);
        void SaveChanges();
    }
}