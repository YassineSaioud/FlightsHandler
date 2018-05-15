using FlightHandler.Core.Dtos;

namespace FlightHandler.Web.Models
{
    public class ReferenceViewModel
    {
        public int Id { get; set; }
        public string Label { get; set; }




        public static implicit operator ReferenceViewModel(ReferenceDto dto)
        {
            return new ReferenceViewModel
            {
                Id = dto.Id,
                Label = dto.Label      
            };
        }

    }
}