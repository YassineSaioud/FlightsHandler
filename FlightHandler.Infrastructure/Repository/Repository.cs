using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace FlightHandler.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DbContext _context;
        internal DbSet<TEntity> dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = dbSet;
            return query;
        }

        public virtual IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return dbSet.Where(predicate).AsQueryable();
        }

        public virtual IQueryable<TEntity> GetWithIncluds(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
                query = includeProperties.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null)
                query = query.Where(predicate).AsQueryable();

            return query;
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                dbSet.Attach(entityToDelete);

            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

    }
}
