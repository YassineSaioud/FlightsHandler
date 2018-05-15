using System;

namespace FlightHandler.Core.Dtos
{
    public class FlightDto
    {
        public int? Id { get; set; }
        public int DeparturAeroportId { get; set; }
        public int ArrivalAeroportId { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
        public double FuelConsumption { get; set; }
    }
}

