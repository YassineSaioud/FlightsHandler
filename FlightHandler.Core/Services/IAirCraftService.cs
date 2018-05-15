using System;
using System.Collections.Generic;
using System.Text;

namespace FlightHandler.Core.Services
{
    public interface IAirCraftService
    {
        double GetAverageSpeed();
        double GetFuelConsumptionWithEffortByHour();
    }
}
