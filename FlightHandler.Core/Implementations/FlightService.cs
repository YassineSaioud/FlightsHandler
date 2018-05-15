using FlightHandler.Core.Dtos;
using FlightHandler.Infrastructure.Models;
using FlightHandler.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightHandler.Core.Services
{
    public class FlightService : IFlightService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IGeoCoordinateService _geoCoordinateService;
        readonly IAirCraftService _airCraftService;

        public FlightService(IUnitOfWork unitOfWork, IGeoCoordinateService geoCoordinateServic, IAirCraftService airCraftService)
        {
            _unitOfWork = unitOfWork;
            _airCraftService = airCraftService;
            _geoCoordinateService = geoCoordinateServic;
        }

        public IEnumerable<FlightDto> GetFlights()
        {
            var flights = from flight in _unitOfWork.GenericRepository<Flight>().GetAll()
                          select new FlightDto
                          {
                              Id = flight.Id,
                              DeparturAeroportId = flight.DeparturAeroportId,
                              ArrivalAeroportId = flight.ArrivalAeroportId,
                              Distance = flight.Distance,
                              Duration = flight.Duration,
                              FuelConsumption = flight.FuelConsumption
                          };
            return flights;
        }

        public void InsertFlight(FlightDto flight)
        {
            var flightToAdd = new Flight
            {
                DeparturAeroportId = flight.DeparturAeroportId,
                ArrivalAeroportId = flight.ArrivalAeroportId
            };
            // Calculate flight infos
            flightToAdd.Distance = GetDistance(flight.DeparturAeroportId, flight.ArrivalAeroportId);
            flightToAdd.Duration = flightToAdd.Distance / _airCraftService.GetAverageSpeed();
            flightToAdd.FuelConsumption = _airCraftService.GetFuelConsumptionWithEffortByHour() / flightToAdd.Duration;
            // Persiste in data base
            _unitOfWork.GenericRepository<Flight>().Insert(flightToAdd);
            _unitOfWork.Save();
        }


        public void EditFlight(FlightDto flight)
        {
            var dbFlight = _unitOfWork.GenericRepository<Flight>().Get(c => c.Id == flight.Id).FirstOrDefault();
            if (dbFlight != null && (dbFlight.DeparturAeroportId != flight.DeparturAeroportId || dbFlight.ArrivalAeroportId != flight.ArrivalAeroportId))
            {
                // Update
                dbFlight.DeparturAeroportId = flight.DeparturAeroportId;
                dbFlight.ArrivalAeroportId = flight.ArrivalAeroportId;
                // ReCalculate flight infos
                dbFlight.Distance = GetDistance(dbFlight.DeparturAeroportId, dbFlight.ArrivalAeroportId);
                dbFlight.Duration = dbFlight.Distance / _airCraftService.GetAverageSpeed();
                dbFlight.FuelConsumption = _airCraftService.GetFuelConsumptionWithEffortByHour() * dbFlight.Duration;
                // Persiste in data base
                _unitOfWork.GenericRepository<Flight>().Update(dbFlight);
                _unitOfWork.Save();
            }
        }

        public FlightDto GetFlightDetail(int id)
        {
            var dbFlight = _unitOfWork.GenericRepository<Flight>().Get(c => c.Id == id).FirstOrDefault();
            return new FlightDto
            {
                Id = dbFlight.Id,
                DeparturAeroportId = dbFlight.DeparturAeroportId,
                ArrivalAeroportId = dbFlight.ArrivalAeroportId,
                Distance = dbFlight.Distance,
                Duration = dbFlight.Duration,
                FuelConsumption = dbFlight.FuelConsumption
            };
        }

        #region Private Methods

        public double GetDistance(int departurAeroportId, int arrivalAeroportId)
        {
            var geoCoordinateFrom = _geoCoordinateService.GetAeroportGPSCoordinate(departurAeroportId);
            var geoCoordinateTo = _geoCoordinateService.GetAeroportGPSCoordinate(arrivalAeroportId);

            if (double.IsNaN(geoCoordinateFrom.Latitude) || double.IsNaN(geoCoordinateFrom.Longitude) || double.IsNaN(geoCoordinateTo.Latitude) || double.IsNaN(geoCoordinateTo.Longitude))
                throw new ArgumentException("Argument latitude or longitude is not a number");

            var d1 = geoCoordinateFrom.Latitude * (Math.PI / 180.0);
            var num1 = geoCoordinateFrom.Longitude * (Math.PI / 180.0);

            var d2 = geoCoordinateTo.Latitude * (Math.PI / 180.0);
            var num2 = geoCoordinateTo.Longitude * (Math.PI / 180.0) - num1;

            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return (6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)))) / 1000;
        }

        #endregion

    }
}
