using FlightHandler.Core.Services;
using FlightHandler.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Flight.Web.Controllers
{
    public class FlightController : Controller
    {
        readonly IFlightService _flightService;
        readonly IReferenceService _referenceService;

        public FlightController(IFlightService flightService, IReferenceService referenceService)
        {
            _flightService = flightService;
            _referenceService = referenceService;
        }

        public ActionResult Index()
        {
            var aeroports = _referenceService.GetAeroports().Select(aeroport => (ReferenceViewModel)aeroport);
            var flightViewModel = new FlightViewModel
            {
                DeparturAeroports = aeroports,
                ArrivalAeroports = aeroports,
            };
            return View(flightViewModel);
        }

        public ActionResult Edit(int id)
        {
            var aeroports = _referenceService.GetAeroports().Select(aeroport => (ReferenceViewModel)aeroport);
            var flight = (FlightViewModel)_flightService.GetFlightDetail(id);

            SetFlightFieldsFormat(aeroports, flight, false);

            return View(flight);
        }

        public ActionResult GetAllFlights()
        {
            var aeroports = _referenceService.GetAeroports().Select(aeroport => (ReferenceViewModel)aeroport);
            var flights = _flightService.GetFlights().Select(flight => (FlightViewModel)flight).ToList();

            foreach (var flight in flights)
                SetFlightFieldsFormat(aeroports, flight, true);

            return View(flights);
        }

        [HttpPost]
        public ActionResult InserOrEditFlight(FlightViewModel vieModel)
        {
            var flightDto = vieModel;

            if (!vieModel.Id.HasValue)
                _flightService.InsertFlight(flightDto);
            else
                _flightService.EditFlight(flightDto);

            return RedirectToAction("GetAllFlights");
        }

        #region Private Methods

        FlightViewModel SetFlightFieldsFormat(IEnumerable<ReferenceViewModel> aeroports, FlightViewModel flight, bool withoutAeroportsList)
        {
            if (!withoutAeroportsList)
            {
                flight.DeparturAeroports = aeroports;
                flight.ArrivalAeroports = aeroports;
            }
            flight.DeparturAeroportName = aeroports.FirstOrDefault(f => f.Id == flight.DeparturAeroportId).Label;
            flight.ArrivalAeroportName = aeroports.FirstOrDefault(f => f.Id == flight.ArrivalAeroportId).Label;
            flight.DistancekilometerFormat = $"{flight.Distance} Km";
            flight.FuelConsumptionLiterFormat = $"{flight.FuelConsumption} L";
            flight.DurationTimeFormat = $"{flight.Duration.ToString().Split(',')[0]} h {flight.Duration.ToString().Split(',')[1].Substring(1, 2)} min";
            return flight;
        }

        #endregion



    }

}