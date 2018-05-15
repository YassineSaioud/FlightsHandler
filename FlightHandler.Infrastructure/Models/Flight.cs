
using System.ComponentModel.DataAnnotations;

namespace FlightHandler.Infrastructure.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public int DeparturAeroportId { get; set; }
        public int ArrivalAeroportId { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
        public double FuelConsumption { get; set; }
    }
}
