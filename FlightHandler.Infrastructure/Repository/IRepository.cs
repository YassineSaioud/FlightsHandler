using System;
using System.Linq;
using System.Linq.Expressions;

namespace FlightHandler.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(Func<TEntity, bool> predicate);
        IQueryable<TEntity> GetWithIncluds(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        void Insert(TEntity entity);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}
