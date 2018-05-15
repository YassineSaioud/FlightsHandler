using FlightHandler.Core.Dtos;
using FlightHandler.Core.Services;
using FlightHandler.Infrastructure.Models;
using FlightHandler.Infrastructure.UnitOfWork;
using System.Linq;

namespace FlightHandler.Core.Implementations
{
    public class GeoCoordinateService : IGeoCoordinateService
    {

        readonly IUnitOfWork _unitOfWork;

        public GeoCoordinateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public GPSCoordinateDto GetAeroportGPSCoordinate(int aeroportId)
        {
            var dbGPSCoordinate = _unitOfWork.GenericRepository<GPSCoordinate>().Get(c => c.AeroportId == aeroportId).FirstOrDefault();
            return new GPSCoordinateDto
            {
                Latitude = dbGPSCoordinate.Latitude,
                Longitude = dbGPSCoordinate.Longitude
            };
        }

    }
}
