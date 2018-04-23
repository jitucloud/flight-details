using FlightOperation.API.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightOperation.API.Interface
{
    /// <summary>
    /// IFlight manager interface
    /// </summary>
    public interface IFlightManager
    {
        Task<List<FlightDetail>> GetAllFlightsList();
        Task<List<FlightDetail>> GetFlightList(DateTime date);
        Task<List<FlightDetail>> CheckAvailbilityOfFlight(DateTime startDate, DateTime endDate, int passengerCount);
        Task<FlightDetail> GetFlightDetails(FlightDetailRequest flightDetails);
    }
}