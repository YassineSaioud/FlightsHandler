using FlightHandler.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightHandler.Core.Services
{
    public interface IGeoCoordinateService
    {
        GPSCoordinateDto GetAeroportGPSCoordinate(int aeroportId);
    }
}
