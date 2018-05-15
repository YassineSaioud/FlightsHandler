using FlightHandler.Core.Implementations;
using FlightHandler.Core.Services;
using FlightHandler.Infrastructure.Context;
using FlightHandler.Infrastructure.UnitOfWork;
using System;

using Unity;

namespace Flight.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {

        #region Unity Container

        static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;

        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();
            container.RegisterSingleton<FlightContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IAirCraftService, AirCraftService>();
            container.RegisterType<IFlightService, FlightService>();
            container.RegisterType<IGeoCoordinateService, GeoCoordinateService>();
            container.RegisterType<IReferenceService, ReferenceService>();
        }
    }
}