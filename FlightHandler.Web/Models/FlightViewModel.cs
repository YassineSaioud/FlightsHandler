using FlightHandler.Core.Dtos;
using System;
using System.Collections.Generic;

namespace FlightHandler.Web.Models
{
    public class FlightViewModel
    {
        public int? Id { get; set; }
        public int DeparturAeroportId { get; set; }
        public int ArrivalAeroportId { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
        public double FuelConsumption { get; set; }
        // Specific to view
        public string DeparturAeroportName { get; set; }
        public string ArrivalAeroportName { get; set; }
        public string DistancekilometerFormat { get; set; }
        public string DurationTimeFormat { get; set; }
        public string FuelConsumptionLiterFormat { get; set; }
        public IEnumerable<ReferenceViewModel> DeparturAeroports { get; set; }
        public IEnumerable<ReferenceViewModel> ArrivalAeroports { get; set; }


        public static implicit operator FlightDto(FlightViewModel viewModel)
        {
            return new FlightDto
            {
                Id = viewModel.Id,
                DeparturAeroportId = viewModel.DeparturAeroportId,
                ArrivalAeroportId = viewModel.ArrivalAeroportId,
                Distance = viewModel.Distance,
                Duration = viewModel.Duration,
                FuelConsumption = viewModel.FuelConsumption
            };
        }

        public static implicit operator FlightViewModel(FlightDto dto)
        {
            return new FlightViewModel
            {
                Id = dto.Id,
                DeparturAeroportId = dto.DeparturAeroportId,
                ArrivalAeroportId = dto.ArrivalAeroportId,
                Distance = dto.Distance,
                Duration = dto.Duration,
                FuelConsumption = dto.FuelConsumption
            };
        }

    }
}

