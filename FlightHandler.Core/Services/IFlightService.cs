using FlightHandler.Core.Dtos;
using System.Collections.Generic;

namespace FlightHandler.Core.Services
{
    public interface IFlightService
    {
        void InsertFlight(FlightDto flight);
        void EditFlight(FlightDto flight);
        FlightDto GetFlightDetail(int id);
        IEnumerable<FlightDto> GetFlights();
    }
}
