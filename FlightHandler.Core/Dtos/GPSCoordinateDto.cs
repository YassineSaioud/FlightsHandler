using System;
using System.Collections.Generic;
using System.Text;

namespace FlightHandler.Core.Dtos
{
    public class GPSCoordinateDto
    {
        public int Id { get; set; }
        public int AeroportId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
