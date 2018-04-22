using Autofac;
using FlightOperation.API.Interface;
using FlightOperation.API.Manager;

namespace FlightOperation.API
{
    /// <summary>
    /// Service Registry to Dependency Injection for all resources
    /// </summary>
    public class ServicesRegistry
    {
        public void Register(ContainerBuilder builder)
        {
            var flightDB = new DbManager("flightbooking");
            var flightManager = new FlightManager(flightDB);
            var passengerManager = new PassengerManager(flightDB);
            var bookingManager = new BookingManager(flightDB, flightManager, passengerManager);


            builder.RegisterInstance<IFlightManager>(flightManager);
            builder.RegisterInstance<IBookingManager>(bookingManager);
            builder.RegisterInstance<IPassengerManager>(passengerManager);


        }
    }
}