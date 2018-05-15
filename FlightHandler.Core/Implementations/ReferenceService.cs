using FlightHandler.Core.Dtos;
using System.Collections.Generic;


namespace FlightHandler.Core.Services
{
    public class ReferenceService : IReferenceService
    {
        public IEnumerable<ReferenceDto> GetAeroports()
        {
            // Fake Data
            return new List<ReferenceDto>
            {
                new ReferenceDto{Id=1, Label="Aeroport 1" },
                new ReferenceDto{Id=2, Label="Aeroport 2" },
                new ReferenceDto{Id=3, Label="Aeroport 3" },
                new ReferenceDto{Id=4, Label="Aeroport 4" },
                new ReferenceDto{Id=5, Label="Aeroport 5" },
            };
        }

    }
}
