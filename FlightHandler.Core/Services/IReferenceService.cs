using FlightHandler.Core.Dtos;
using System.Collections.Generic;

namespace FlightHandler.Core.Services
{
    public interface IReferenceService
    {
        IEnumerable<ReferenceDto> GetAeroports();
    }
}
