using FlightHandler.Infrastructure.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FlightHandler.Infrastructure.Context
{
    public class FlightContext : DbContext
    {

        public FlightContext() : base("FlightHandlerContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<GPSCoordinate> GPSCoordinates { get; set; }

    }
}
