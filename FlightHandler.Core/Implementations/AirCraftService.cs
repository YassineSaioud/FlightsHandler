using FlightHandler.Core.Services;

namespace FlightHandler.Core.Implementations
{
    public class AirCraftService : IAirCraftService
    {
        // Fake Data Km/Hour
        public double GetAverageSpeed()
        {
            return 650;
        }

        // Fake Data Liter/Hour
        public double GetFuelConsumptionWithEffortByHour()
        {
            return 5;
        }
    }
}
