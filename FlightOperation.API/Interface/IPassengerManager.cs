using FlightOperation.API.Model;
using System.Threading.Tasks;

namespace FlightOperation.API.Interface
{
    /// <summary>
    /// IPassenger Manager interface
    /// </summary>
    public interface IPassengerManager
    {
        Task<int> CreatePassenger(Passenger passenger);
        Task<bool> UpdatePassengerBookingRecord(int passengerId, string pnr);
    }
}
