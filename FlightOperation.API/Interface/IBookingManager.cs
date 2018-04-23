using FlightOperation.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace FlightOperation.API.Interface
{
    /// <summary>
    /// Booking manager interface
    /// </summary>
    public interface IBookingManager
    {
        Task<List<Booking>> SearchBooking(SearchBookingModel search);
        Task<Tuple<HttpStatusCode, String>> MakeBooking(CreateBookingRequestDeatils Booking);
    }
}