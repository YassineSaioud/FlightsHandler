
using System.ComponentModel.DataAnnotations;

namespace FlightHandler.Infrastructure.Models
{
    public class GPSCoordinate
    {
        [Key]
        public int Id { get; set; }
        public int AeroportId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

