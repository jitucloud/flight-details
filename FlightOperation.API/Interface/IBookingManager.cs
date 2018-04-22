using FlightOperation.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace FlightOperation.API.Interface
{
    public interface IBookingManager
    {
        Task<List<Booking>> SearchBooking();
        Task<Tuple<HttpStatusCode, String>> MakeBooking(BookingRequestDeatils Booking);
    }
}