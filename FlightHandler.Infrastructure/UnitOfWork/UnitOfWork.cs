using FlightHandler.Infrastructure.Context;
using FlightHandler.Infrastructure.Repository;

namespace FlightHandler.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly FlightContext _context;

        public UnitOfWork(FlightContext context)
        {
            _context = context;
        }

        public IRepository<T> GenericRepository<T>() where T : class
        {
            return new Repository<T>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
