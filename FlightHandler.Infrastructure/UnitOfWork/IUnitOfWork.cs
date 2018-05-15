using FlightHandler.Infrastructure.Repository;

namespace FlightHandler.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<T> GenericRepository<T>() where T : class;
        void Save();
    }
}
